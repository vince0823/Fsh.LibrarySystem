using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookByIdSpec : Specification<Book>, ISingleResultSpecification
{
    public BookByIdSpec(Guid bookId)
    {
        Query.Where(b => b.Id == bookId);
        Query.Include(v => v.Items);
    }
}
