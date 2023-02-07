using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class MenuByParentIdNullSpec : Specification<Menu>
{
    public MenuByParentIdNullSpec() =>
      Query.Where(p => p.ParentId == null).OrderBy(p => p.Order);
}
