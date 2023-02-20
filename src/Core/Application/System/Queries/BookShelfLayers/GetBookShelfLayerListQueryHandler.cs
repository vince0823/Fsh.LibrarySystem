using FSH.Learn.Application.System.Queries.BookShelfs;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfLayers;
public class GetBookShelfLayerListQueryHandler : IRequestHandler<GetBookShelfLayerListQuery, PaginationResponse<BookShelfLayerDto>>
{
    private readonly IReadRepository<BookShelfLayer> _repository;

    public GetBookShelfLayerListQueryHandler(IReadRepository<BookShelfLayer> repository) => _repository = repository;

    public async Task<PaginationResponse<BookShelfLayerDto>> Handle(GetBookShelfLayerListQuery query, CancellationToken cancellationToken)
    {
        var spec = new BookShelfLayerBySearchSpec(query);
        return await _repository.PaginatedListAsync(spec, query.PageNumber, query.PageSize, cancellationToken: cancellationToken);
    }
}