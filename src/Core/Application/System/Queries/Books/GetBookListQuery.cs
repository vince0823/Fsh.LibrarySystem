using FSH.Learn.Application.System.Queries.BookShelfLayers;
using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.Books;
public class GetBookListQuery : PaginationFilter, IRequest<PaginationResponse<BookDto>>
{
    public string? Name { get; set; }
    public string? Author { get; set; }
    public bool? IsBorrowed { get; set; }
    public BookType? BookType { get; set; }
}
