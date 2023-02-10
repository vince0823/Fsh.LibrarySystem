using FSH.Learn.Application.System.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookRooms;
public class GetBookRoomListQuery : PaginationFilter, IRequest<PaginationResponse<BookRoomDto>>
{
    public string? Name { get; set; }
}
