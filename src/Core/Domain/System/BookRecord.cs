using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System;
public class BookRecord : BaseEntity
{
    public Guid BookId { get; set; }
    public BookRecordType BookRecordType { get; set; }
    public string CreateUserId { get; set; } = default!;
    public DateTime CreateDateTime { get; set; }
    public BookRecord()
    {
    }

    public BookRecord(Guid bookId, BookRecordType bookRecordType, string createUserId)
    {
        BookId = bookId;
        BookRecordType = bookRecordType;
        CreateUserId = createUserId;
        CreateDateTime = DateTime.Now;
    }

}

