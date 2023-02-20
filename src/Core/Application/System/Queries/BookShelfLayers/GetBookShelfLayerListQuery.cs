using FSH.Learn.Application.System.Queries.BookShelfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfLayers;
public class GetBookShelfLayerListQuery : PaginationFilter, IRequest<PaginationResponse<BookShelfLayerDto>>
{
    public string? LayerName { get; set; }
}
