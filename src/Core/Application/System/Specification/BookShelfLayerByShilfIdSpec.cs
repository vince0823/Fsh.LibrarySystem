using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookShelfLayerByShilfIdSpec : Specification<BookShelfLayer>, ISingleResultSpecification
{
    public BookShelfLayerByShilfIdSpec(Guid bookShilfId)
    {
        Query.Where(t => t.BookShelfId == bookShilfId);
    }
}
