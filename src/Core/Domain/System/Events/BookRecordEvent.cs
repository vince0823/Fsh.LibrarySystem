using FSH.Learn.Domain.Catalog;
using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System.Events;
public class BookRecordEvent : DomainEvent
{
    public Guid BookId { get; set; }
    public BookRecordType BookRecordType { get; set; }
    protected BookRecordEvent(Guid bookId, BookRecordType bookRecordType) => (BookId, BookRecordType) = (bookId, bookRecordType);
}

public class BookRecordAddEvent : BookRecordEvent
{
    public BookRecordAddEvent(Guid bookId, BookRecordType bookRecordType)
        : base(bookId, bookRecordType)
    {

    }
}
