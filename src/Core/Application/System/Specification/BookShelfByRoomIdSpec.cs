using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookShelfByRoomIdSpec : Specification<BookShelf>, ISingleResultSpecification
{
    public BookShelfByRoomIdSpec(Guid roomId)
    {
        Query.Where(t => t.BookRoomId == roomId);
    }
}
