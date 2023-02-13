using FSH.Learn.Application.System.Queries.BookRooms;
using FSH.Learn.Application.System.Queries.BookShelfs;
using FSH.Learn.Domain.System;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookShelfBySearchSpec : EntitiesByPaginationFilterSpec<BookShelf, BookShelfDto>
{
    public BookShelfBySearchSpec(GetBookShelfListQuery query)
        : base(query)
    {
        Query
           .Include(p => p.BookRoom)
           .OrderBy(c => c.CreatedOn, !query.HasOrderBy())
        .Where(p => p.Code.Contains(query.Code), !string.IsNullOrEmpty(query.Code));
    }
}
