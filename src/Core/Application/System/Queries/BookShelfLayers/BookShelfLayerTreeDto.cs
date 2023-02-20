using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfLayers;
public class BookShelfLayerTreeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<BookShelfLayerTreeDto> Children { get; set; }
}
