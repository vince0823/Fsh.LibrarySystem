using FSH.Learn.Application.System.Queries.BookRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfs;
public class GetBookShelfListQuery : PaginationFilter, IRequest<PaginationResponse<BookShelfDto>>
{
    public string? Code { get; set; }
}
