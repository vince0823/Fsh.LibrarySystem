using FSH.Learn.Application.System.Queries.BookShelfLayers;
using FSH.Learn.Application.System.Queries.BookShelfs;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookShelfLayerBySearchSpec : EntitiesByPaginationFilterSpec<BookShelfLayer, BookShelfLayerDto>
{
    public BookShelfLayerBySearchSpec(GetBookShelfLayerListQuery query)
        : base(query)
    {
        Query
           .Include(p => p.BookShelf)
           .OrderBy(c => c.CreatedOn, !query.HasOrderBy())
        .Where(p => p.LayerName.Contains(query.LayerName), !string.IsNullOrEmpty(query.LayerName));
    }
}