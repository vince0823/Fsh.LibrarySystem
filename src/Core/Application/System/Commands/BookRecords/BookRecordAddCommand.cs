using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookRecords;
public class BookRecordAddCommand : IRequest<Guid>
{
    public Guid BookId { get; set; }

    public BookRecordType BookRecordType { get; set; }

    public BookRecordAddCommand(Guid bookId, BookRecordType bookRecordType)
    {
        BookId = bookId;
        BookRecordType = bookRecordType;
    }

}
