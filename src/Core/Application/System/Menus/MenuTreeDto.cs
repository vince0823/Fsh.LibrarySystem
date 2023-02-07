using FSH.Learn.Application.System.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class MenuTreeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; } = default!;
    public string Icon { get; set; } = default!;
    public Guid? ParentId { get; set; }
    public List<ChildMenu>? ChildMenuList { get; set; }
}

public class ChildMenu
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Icon { get; set; } = default!;
    public List<ChildMenu>? ChildMenuList { get; set; }
}
