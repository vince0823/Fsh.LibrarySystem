using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.Identity;
public abstract class RoleMenuEvent : DomainEvent
{
    public string RoleId { get; set; } = default!;
    public List<string> MenuNames { get; set; } = default!;

    protected RoleMenuEvent(string roleId, List<string> menuNames)
    {
        RoleId = roleId;
        MenuNames = menuNames;
    }
}

public class UpdateRoleMenuEvent : RoleMenuEvent
{
    public UpdateRoleMenuEvent(string roleId, List<string> menuNames)
        : base(roleId, menuNames)
    {
    }
}
