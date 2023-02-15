using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System;
public class BookShelfLayer : AuditableEntity, IAggregateRoot
{

    public string LayerName { get; set; } = default!;
    public Guid BookShelfId { get; set; }
    public virtual BookShelf BookShelf { get; set; } = default!;

    public BookShelfLayer()
    {

    }

    public BookShelfLayer(string layerName, Guid bookShelfId)
    {
        LayerName = layerName;
        BookShelfId = bookShelfId;
    }

    public BookShelfLayer Update(string? layerName, Guid? bookShelfId)
    {
        if (layerName is not null && LayerName?.Equals(layerName) is not true) LayerName = layerName;
        if (bookShelfId.HasValue && bookShelfId.Value != Guid.Empty && !BookShelfId.Equals(bookShelfId.Value)) BookShelfId = bookShelfId.Value;
        return this;
    }
}
