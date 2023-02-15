using FSH.Learn.Application.Common.Exceptions;
using FSH.Learn.Application.System.Menus;
using FSH.Learn.Domain.System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Infrastructure.Identity;
internal partial class UserService
{
    public async Task<List<MenuTreeDto>> GetMeunsAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);
        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);
        var userRoles = await _userManager.GetRolesAsync(user);

        // 用户角色列表
        var roleList = await _roleManager.Roles.Where(r => userRoles.Contains(r.Name)).ToListAsync(cancellationToken);

        var menuIdList = await _db.RoleMenus.Where(t => roleList.Select(t => t.Id).Contains(t.RoleId)).Select(t => t.MenuId).ToListAsync(cancellationToken);
        var dtoList = new List<MenuTreeDto>();
        var allmenuList = await _db.Menus.Where(t => menuIdList.Contains(t.Id)).OrderBy(t => t.Order).ToListAsync(cancellationToken);
        foreach (var parentMenu in allmenuList.Where(t => t.ParentId == null).OrderBy(t => t.Order).ToList())
        {
            dtoList.Add(new MenuTreeDto
            {
                Id = parentMenu.Id,
                Name = parentMenu.Name,
                DisPlayName = parentMenu.DisPlayName,
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
                DisPlayName = menu.DisPlayName,
                Url = menu.Url,
                Icon = menu.Icon,
                ParentId = menu.ParentId,
                ChildMenuList = await GetChildren(menu.Id, allmenuList)
            });
        }

        return list;
    }
}
