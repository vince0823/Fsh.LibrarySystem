using FSH.Learn.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FSH.Learn.Domain.System;
public class BookShelf : AuditableEntity, IAggregateRoot
{
    /// <summary>
    /// 书架编号.
    /// </summary>
    public string Code { get; set; } = default!;
    public Guid BookRoomId { get; set; }
    public virtual BookRoom BookRoom { get; set; } = default!;

    public BookShelf()
    {
    }

    public BookShelf(string code, Guid bookRoomId)
    {
        Code = code;
        BookRoomId = bookRoomId;
    }

    public BookShelf Update(string? code, Guid? bookRoomId)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (bookRoomId.HasValue && bookRoomId.Value != Guid.Empty && !BookRoomId.Equals(bookRoomId.Value)) BookRoomId = bookRoomId.Value;
        return this;
    }
}
