using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class DepartmentsBySearchRequestWithBrandsSpec : EntitiesByPaginationFilterSpec<Department, DepartmentDto>
{
    public DepartmentsBySearchRequestWithBrandsSpec(SearchDepartmentsRequest request)
        : base(request) =>
        Query
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p =>  p.DutyUserId.Contains(request.DutyUserId), !string.IsNullOrEmpty(request.DutyUserId))
          ;
}
