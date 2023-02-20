using FSH.Learn.Application.System.Queries.BookShelfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfLayers;
public class GetBookShelfLayerQuery : IRequest<BookShelfLayerDto>
{
    public Guid Id { get; set; }

    public GetBookShelfLayerQuery(Guid id) => Id = id;
}
