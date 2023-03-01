using Ardalis.Specification;
using FSH.Learn.Application.System.Queries.BookRooms;
using FSH.Learn.Application.System.Queries.Books;
using FSH.Learn.Domain.System;
using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookBySearchSpec : EntitiesByPaginationFilterSpec<Book, BookDto>
{
    public BookBySearchSpec(GetBookListQuery query)
: base(query) =>
       Query.Where(p => p.Name.Contains(query.Name), !string.IsNullOrEmpty(query.Name))
       .Where(p => p.Author.Contains(query.Author), !string.IsNullOrEmpty(query.Author))
       .Where(p => p.IsBorrowed == query.IsBorrowed.Value, query.IsBorrowed.HasValue)
       .Where(p => p.BookType == query.BookType.Value, query.BookType.HasValue && Enum.IsDefined(typeof(BookType), query.BookType));
}
