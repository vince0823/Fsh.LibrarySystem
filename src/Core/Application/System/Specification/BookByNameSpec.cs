using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
internal class BookByNameSpec : Specification<Book>, ISingleResultSpecification
{
    public BookByNameSpec(string name, string author) =>
       Query.Where(b => b.Name == name && b.Author == author);

}