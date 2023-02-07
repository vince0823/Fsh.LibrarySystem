using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class MenuByNameSpec : Specification<Menu>, ISingleResultSpecification
{
    public MenuByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}
