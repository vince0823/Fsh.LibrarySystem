using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Finbuckle.MultiTenant;
using FSH.Learn.Application.Common.Events;
using FSH.Learn.Application.Common.Exceptions;
using FSH.Learn.Application.Common.Interfaces;
using FSH.Learn.Application.Identity;
using FSH.Learn.Application.Identity.Roles;
using FSH.Learn.Application.System.Menus;
using FSH.Learn.Domain.Identity;
using FSH.Learn.Domain.System;
using FSH.Learn.Infrastructure.Persistence.Context;
using FSH.Learn.Shared.Authorization;
using FSH.Learn.Shared.Multitenancy;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace FSH.Learn.Infrastructure.Identity;

internal class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly IStringLocalizer<RoleService> _localizer;
    private readonly ICurrentUser _currentUser;
    private readonly ITenantInfo _currentTenant;
    private readonly IEventPublisher _events;

    public RoleService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext db,
        IStringLocalizer<RoleService> localizer,
        ICurrentUser currentUser,
        ITenantInfo currentTenant,
        IEventPublisher events)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
        _localizer = localizer;
        _currentUser = currentUser;
        _currentTenant = currentTenant;
        _events = events;
    }

    public async Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken) =>
        (await _roleManager.Roles.ToListAsync(cancellationToken))
            .Adapt<List<RoleDto>>();

    public async Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        await _roleManager.Roles.CountAsync(cancellationToken);

    public async Task<bool> ExistsAsync(string roleName, string? excludeId) =>
        await _roleManager.FindByNameAsync(roleName)
            is ApplicationRole existingRole
            && existingRole.Id != excludeId;

    public async Task<RoleDto> GetByIdAsync(string id) =>
        await _db.Roles.SingleOrDefaultAsync(x => x.Id == id) is { } role
            ? role.Adapt<RoleDto>()
            : throw new NotFoundException(_localizer["Role Not Found"]);

    public async Task<RoleDto> GetByIdWithPermissionsAsync(string roleId, CancellationToken cancellationToken)
    {
        var role = await GetByIdAsync(roleId);

        role.Permissions = await _db.RoleClaims
            .Where(c => c.RoleId == roleId && c.ClaimType == FSHClaims.Permission)
            .Select(c => c.ClaimValue)
            .ToListAsync(cancellationToken);

        return role;
    }

    public async Task<List<MenuTreeDto>> GetByIdWithMenusAsync(string roleId, CancellationToken cancellationToken)
    {
        var role = await GetByIdAsync(roleId);
        var menuIdList = await _db.RoleMenus.Where(t => t.RoleId == role.Id).Select(t => t.MenuId).ToListAsync(cancellationToken);
        var dtoList = new List<MenuTreeDto>();
        var allmenuList = await _db.Menus.Where(t => menuIdList.Contains(t.Id)).OrderBy(t => t.Order).ToListAsync(cancellationToken);
        foreach (var parentMenu in allmenuList.Where(t => t.ParentId == null).OrderBy(t => t.Order).ToList())
        {
            dtoList.Add(new MenuTreeDto
            {
                Id = parentMenu.Id,
                Name = parentMenu.Name,
                Url = parentMenu.Url,
                Icon = parentMenu.Icon,
                ParentId = parentMenu.ParentId,
                ChildMenuList = await GetChildren(parentMenu.Id, allmenuList)
            });
        }

        return dtoList;
    }

    private async Task<List<ChildMenu>> GetChildren(Guid pid, List<Menu> allmenuList)
    {
        List<Menu> depList = allmenuList.Where(t => t.ParentId == pid).OrderBy(t => t.Order).ToList();
        List<ChildMenu> list = new List<ChildMenu>();
        foreach (Menu menu in depList)
        {
            list.Add(new ChildMenu
            {
                Id = menu.Id,
                Name = menu.Name,
                Url = menu.Url,
                Icon = menu.Icon,
                ParentId = menu.ParentId,
                ChildMenuList = await GetChildren(menu.Id, allmenuList)
            });
        }

        return list;
    }

    public async Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            // Create a new role.
            var role = new ApplicationRole(request.Name, request.Description);
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException(_localizer["Register role failed"], result.GetErrors(_localizer));
            }

            await _events.PublishAsync(new ApplicationRoleCreatedEvent(role.Id, role.Name));

            return string.Format(_localizer["Role {0} Created."], request.Name);
        }
        else
        {
            // Update an existing role.
            var role = await _roleManager.FindByIdAsync(request.Id);

            _ = role ?? throw new NotFoundException(_localizer["Role Not Found"]);

            if (FSHRoles.IsDefault(role.Name))
            {
                throw new ConflictException(string.Format(_localizer["Not allowed to modify {0} Role."], role.Name));
            }

            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpperInvariant();
            role.Description = request.Description;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException(_localizer["Update role failed"], result.GetErrors(_localizer));
            }

            await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Id, role.Name));

            return string.Format(_localizer["Role {0} Updated."], role.Name);
        }
    }

    public async Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        _ = role ?? throw new NotFoundException(_localizer["Role Not Found"]);
        if (role.Name == FSHRoles.Admin)
        {
            throw new ConflictException(_localizer["Not allowed to modify Permissions for this Role."]);
        }

        if (_currentTenant.Id != MultitenancyConstants.Root.Id)
        {
            // Remove Root Permissions if the Role is not created for Root Tenant.
            request.Permissions.RemoveAll(u => u.StartsWith("Permissions.Root."));
        }

        var currentClaims = await _roleManager.GetClaimsAsync(role);

        // Remove permissions that were previously selected
        foreach (var claim in currentClaims.Where(c => !request.Permissions.Any(p => p == c.Value)))
        {
            var removeResult = await _roleManager.RemoveClaimAsync(role, claim);
            if (!removeResult.Succeeded)
            {
                throw new InternalServerException(_localizer["Update permissions failed."], removeResult.GetErrors(_localizer));
            }
        }

        // Add all permissions that were not previously selected
        foreach (string permission in request.Permissions.Where(c => !currentClaims.Any(p => p.Value == c)))
        {
            if (!string.IsNullOrEmpty(permission))
            {
                _db.RoleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = FSHClaims.Permission,
                    ClaimValue = permission,
                    CreatedBy = _currentUser.GetUserId().ToString()
                });
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Id, role.Name, true));

        // 修改角色菜单
        var menuList = _db.Menus.AsNoTracking().ToList();
        var selectMenuList = menuList.Where(t => request.MenuNames.Contains(t.Name)).ToList();
        List<Guid> menuIDList = new List<Guid>();
        foreach (var menu in selectMenuList)
        {
            List<Guid> selectMenuIdList = new List<Guid>();
            GetParents(menu, menuList, ref selectMenuIdList);
            menuIDList.AddRange(selectMenuIdList);
        }

        UpdateRoleMenusRequest updateRoleMenusRequest = new UpdateRoleMenusRequest()
        {
            RoleId = request.RoleId,
            MenuIdList = menuIDList.Distinct().ToList()
        };
        await UpdateRoleMenusAsync(updateRoleMenusRequest, cancellationToken);
        return _localizer["Permissions Updated."];
    }

    private void GetParents(Menu menu, List<Menu> menus, ref List<Guid> MenuIdList)
    {
        MenuIdList.Add(menu.Id);
        var parentMenu = menus.FirstOrDefault(x => x.Id == menu.ParentId);
        if (parentMenu != null)
        {
            //递归
            GetParents(parentMenu, menus, ref MenuIdList);
        }

    }



    public async Task<string> UpdateRoleMenusAsync(UpdateRoleMenusRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        _ = role ?? throw new NotFoundException(_localizer["Role Not Found"]);
        if (role.Name == FSHRoles.Admin)
        {
            throw new ConflictException(_localizer["Not allowed to modify Menu for this Role."]);
        }

        // 获取该角色的所有菜单
        var roleMenuIDList = await _db.RoleMenus.Where(t => t.RoleId == role.Id).Select(t => t.MenuId).ToListAsync();

        // Remove menus that were previously selected
        var selectMenuIdList = roleMenuIDList.Where(c => !request.MenuIdList.Any(p => p == c)).ToList();
        _db.RoleMenus.RemoveRange(await _db.RoleMenus.Where(t => t.RoleId == role.Id && selectMenuIdList.Contains(t.MenuId)).ToListAsync());
        await _db.SaveChangesAsync(cancellationToken);

        // Add all menus that were not previously selected
        foreach (var menuId in request.MenuIdList.Where(c => !roleMenuIDList.Any(p => p == c)))
        {
            _db.RoleMenus.Add(new RoleMenu
            {
                RoleId = role.Id,
                MenuId = menuId,
            });
            await _db.SaveChangesAsync(cancellationToken);

        }

        await _events.PublishAsync(new ApplicationRoleUpdatedMenuEvent(role.Id, role.Name, true));

        return _localizer["rolemenus Updated."];
    }

    public async Task<string> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        _ = role ?? throw new NotFoundException(_localizer["Role Not Found"]);

        if (FSHRoles.IsDefault(role.Name))
        {
            throw new ConflictException(string.Format(_localizer["Not allowed to delete {0} Role."], role.Name));
        }

        if ((await _userManager.GetUsersInRoleAsync(role.Name)).Count > 0)
        {
            throw new ConflictException(string.Format(_localizer["Not allowed to delete {0} Role as it is being used."], role.Name));
        }

        await _roleManager.DeleteAsync(role);

        await _events.PublishAsync(new ApplicationRoleDeletedEvent(role.Id, role.Name));

        return string.Format(_localizer["Role {0} Deleted."], role.Name);
    }
}