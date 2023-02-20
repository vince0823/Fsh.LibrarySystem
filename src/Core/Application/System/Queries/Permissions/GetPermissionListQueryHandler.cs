using FSH.Learn.Application.Dashboard;
using FSH.Learn.Shared.Authorization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FSH.Learn.Application.System.Queries.Permissions;
public class GetPermissionListQueryHandler : IRequestHandler<GetPermissionListQuery, List<PermissionDto>>
{
    public async Task<List<PermissionDto>> Handle(GetPermissionListQuery request, CancellationToken cancellationToken)
    {
        Dictionary<string, string> pairs = new Dictionary<string, string>
        {
            { "Tenants", "租户" },
            { "Dashboard", "首页" },
            { "Hangfire", "后台任务" },
            { "Users", "用户" },
            { "UserRoles", "用户角色" },
            { "Roles", "角色" },
            { "RoleClaims", "角色权限" },
            { "RoleMenu", "角色菜单" },
            { "Products", "产品" },
            { "Brands", "商标" },
            { "Departments", "部门" },
            { "Menus", "菜单" },
            { "BookRooms", "书屋" },
            { "BookShelfs", "书架" },
            { "BookShelfLayers", "书层" },
        };

        var values = typeof(FSHResource).GetFields(BindingFlags.Static | BindingFlags.Public)
                                 .Where(x => x.IsLiteral && !x.IsInitOnly)
                                 .Select(x => x.GetValue(null)).Cast<string>();
        var permissions = new List<PermissionDto>();
        foreach (string field in values)
        {
            var childPermissions = new List<PermissionDto>();
            foreach (var item in FSHPermissions.All)
            {

                if (field == item.Resource)
                {

                    childPermissions.Add(new PermissionDto()
                    {
                        Name = item.Name,
                        DisPlayName = item.Description,
                        Children = new List<PermissionDto>()
                    });

                }
            }

            permissions.Add(new PermissionDto()
            {
                Name = field,
                DisPlayName = pairs.SingleOrDefault(t => t.Key == field).Value,
                Children = childPermissions
            });
        }

        return permissions;

    }
}
