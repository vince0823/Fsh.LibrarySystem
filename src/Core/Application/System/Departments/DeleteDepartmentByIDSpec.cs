using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class DeleteDepartmentByIDSpec : Specification<Department>
{
    public DeleteDepartmentByIDSpec(List<Guid> IdList) =>
      Query.Where(p => IdList.Contains(p.Id));
}
