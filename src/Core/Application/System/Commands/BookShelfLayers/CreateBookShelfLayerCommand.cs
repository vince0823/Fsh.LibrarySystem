using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfLayers;
public class CreateBookShelfLayerCommand : IRequest<Guid>
{
    /// <summary>
    /// 书层名称.
    /// </summary>
    public string LayerName { get; set; } = default!;

    /// <summary>
    /// 书架Id.
    /// </summary>
    public Guid BookShelfId { get; set; }
}
