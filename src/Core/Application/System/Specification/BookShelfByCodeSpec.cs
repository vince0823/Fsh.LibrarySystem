using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookShelfByCodeSpec : Specification<BookShelf>, ISingleResultSpecification
{
    public BookShelfByCodeSpec(string code, Guid bookRoomId) =>
      Query.Where(b => b.Code == code && b.BookRoomId == bookRoomId);
    public BookShelfByCodeSpec(string code, Guid bookRoomId, Guid oldId) =>
     Query.Where(b => b.Code == code && b.BookRoomId == bookRoomId && b.Id != oldId);

}
