using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Identity.Roles;
public class UpdateRoleMenusRequest
{
    public string RoleId { get; set; } = default!;
    public List<Guid> MenuIdList { get; set; } = default!;

}

public class UpdateRoleMenusRequestValidator : CustomValidator<UpdateRoleMenusRequest>
{
    public UpdateRoleMenusRequestValidator()
    {
        RuleFor(r => r.RoleId)
            .NotEmpty();
        RuleFor(r => r.MenuIdList)
            .NotNull();
    }
}
