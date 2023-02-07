using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System;
public class RoleMenu : BaseEntity
{
    public string RoleId { get; set; } = default!;
    public Guid MenuId { get; set; }
    [NotMapped]
    public List<DomainEvent> DomainEvents { get; } = new();
}
