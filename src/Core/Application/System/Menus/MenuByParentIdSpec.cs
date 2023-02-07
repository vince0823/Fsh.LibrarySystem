using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class MenuByParentIdSpec : Specification<Menu>
{
    public MenuByParentIdSpec(Guid parentId) =>
      Query.Where(p => p.ParentId == parentId).OrderBy(p => p.Order);
}
