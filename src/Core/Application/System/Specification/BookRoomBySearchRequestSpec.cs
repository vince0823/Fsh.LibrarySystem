using FSH.Learn.Application.System.Departments;
using FSH.Learn.Application.System.Queries.BookRooms;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookRoomBySearchRequestSpec : EntitiesByPaginationFilterSpec<BookRoom, BookRoomDto>
{
    public BookRoomBySearchRequestSpec(GetBookRoomListQuery query)
       : base(query) =>
       Query.Where(p => p.Name.Contains(query.Name), !string.IsNullOrEmpty(query.Name));
}
