using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class DepartmentTreeDto
{

    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid? ParentId { get; set; }
    public List<ChildDepartment>? ChildDepartmentList { get; set; }
}

public class ChildDepartment
{
    public Guid Id { get; set; }
    public Guid ?ParentId { get; set; }
    public string Name { get; set; } = default!;
    public List<ChildDepartment>? ChildDepartmentList { get; set; }
}
