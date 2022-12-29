using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class DepartmentDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public string ParentName { get; set; } = default!;
    public string? DutyUserId { get; set; }
    public string DutyUserName { get; set; } = default!;
}
