using FSH.Learn.Application.Common.Events;
using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.Identity.Roles;
using FSH.Learn.Domain.Catalog;
using FSH.Learn.Domain.Identity;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Infrastructure.Identity;
internal class InvalidateUpdateRoleMenuHandler : IEventNotificationHandler<UpdateRoleMenuEvent>
{

    private readonly IRoleService _roleService;
    private readonly IRepository<Menu> _repository;
    public InvalidateUpdateRoleMenuHandler(IRoleService roleService, IRepository<Menu> repository)
    {
        _roleService = roleService;
        _repository = repository;
    }

    public async Task Handle(EventNotification<UpdateRoleMenuEvent> notification, CancellationToken cancellationToken)
    {
        var menuList = await _repository.ListAsync(cancellationToken);
        var selectMenuList = menuList.Where(t => notification.Event.MenuNames.Contains(t.Name)).ToList();
        List<Guid> menuIDList = new List<Guid>();
        foreach (var menu in selectMenuList)
        {
            List<Guid> selectMenuIdList = new List<Guid>();
            GetParents(menu, menuList, ref selectMenuIdList);
            menuIDList.AddRange(selectMenuIdList);
        }

        UpdateRoleMenusRequest updateRoleMenusRequest = new UpdateRoleMenusRequest()
        {
            RoleId = notification.Event.RoleId,
            MenuIdList = menuIDList.Distinct().ToList()
        };

        await _roleService.UpdateRoleMenusAsync(updateRoleMenusRequest, cancellationToken);
    }

    private void GetParents(Menu menu, List<Menu> menus, ref List<Guid> MenuIdList)
    {
        MenuIdList.Add(menu.Id);
        var parentMenu = menus.FirstOrDefault(x => x.Id == menu.ParentId);
        if (parentMenu != null)
        {
            // 递归

            GetParents(parentMenu, menus, ref MenuIdList);
        }

    }
}
