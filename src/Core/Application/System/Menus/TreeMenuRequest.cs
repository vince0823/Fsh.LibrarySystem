using FSH.Learn.Application.System.Departments;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class TreeMenuRequest : IRequest<List<MenuTreeDto>>
{

}

public class TreeMenuRequestHandler : IRequestHandler<TreeMenuRequest, List<MenuTreeDto>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepository<Menu> _menuRepo;
    private readonly IStringLocalizer<TreeMenuRequestHandler> _localizer;

    public TreeMenuRequestHandler(IRepository<Menu> menuRepo, IStringLocalizer<TreeMenuRequestHandler> localizer) =>
        (_menuRepo, _localizer) = (menuRepo, localizer);

    public async Task<List<MenuTreeDto>> Handle(TreeMenuRequest request, CancellationToken cancellationToken)
    {

        var dtoList = new List<MenuTreeDto>();
        var parentMenuList = await _menuRepo.ListAsync(new MenuByParentIdNullSpec(), cancellationToken);
        foreach (var parentMenu in parentMenuList)
        {
            dtoList.Add(new MenuTreeDto
            {
                Id = parentMenu.Id,
                Name = parentMenu.Name,
                Url= parentMenu.Url,
                Icon= parentMenu.Icon,
                ParentId = parentMenu.ParentId,
                ChildMenuList = await GetChildren(parentMenu.Id)
            });
        }

        return dtoList;
    }

    private async Task<List<ChildMenu>> GetChildren(Guid pid)
    {
        List<Menu> depList = await _menuRepo.ListAsync(new MenuByParentIdSpec(pid));
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
                ChildMenuList = await GetChildren(menu.Id)
            });
        }

        return list;
    }
}
