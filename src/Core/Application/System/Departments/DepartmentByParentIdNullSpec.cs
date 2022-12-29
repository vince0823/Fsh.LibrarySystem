using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class DepartmentByParentIdNullSpec : Specification<Department>
{
    public DepartmentByParentIdNullSpec() =>
      Query.Where(p => p.ParentId == null);
}
