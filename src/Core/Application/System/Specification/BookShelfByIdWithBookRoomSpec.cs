using FSH.Learn.Application.System.Queries.BookShelfs;
using FSH.Learn.Domain.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookShelfByIdWithBookRoomSpec : Specification<BookShelf, BookShelfDetailDto>, ISingleResultSpecification
{
    public BookShelfByIdWithBookRoomSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.BookRoom);
}