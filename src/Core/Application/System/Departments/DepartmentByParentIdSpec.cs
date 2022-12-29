using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class DepartmentByParentIdSpec : Specification<Department>
{
    public DepartmentByParentIdSpec(Guid parentId) =>
      Query.Where(p => p.ParentId == parentId);
}
