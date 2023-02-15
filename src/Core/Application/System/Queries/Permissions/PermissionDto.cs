using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.Permissions;
public class PermissionDto
{
    public string Name { get; set; } = default!;
    public string? DisPlayName { get; set; }
    public ICollection<PermissionDto> Children { get; set; }
}
