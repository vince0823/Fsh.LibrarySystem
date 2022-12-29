using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class DepartmentByNameSpec : Specification<Department>, ISingleResultSpecification
{
    public DepartmentByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}
