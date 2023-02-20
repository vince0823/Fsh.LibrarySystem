using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfLayers;
public class BookShelfLayerDto : IDto
{
    public Guid Id { get; set; }
    public string LayerName { get; set; } = default!;
    public Guid BookShelfId { get; set; }
    public string BookShelfCode { get; set; } = default!;
    public string Address { get; set; } = default!;
}
