using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfs;
public class GetBookShelfListQueryHandler : IRequestHandler<GetBookShelfListQuery, PaginationResponse<BookShelfDto>>
{
    private readonly IReadRepository<BookShelf> _repository;

    public GetBookShelfListQueryHandler(IReadRepository<BookShelf> repository) => _repository = repository;

    public async Task<PaginationResponse<BookShelfDto>> Handle(GetBookShelfListQuery query, CancellationToken cancellationToken)
    {
        var spec = new BookShelfBySearchSpec(query);
        return await _repository.PaginatedListAsync(spec, query.PageNumber, query.PageSize, cancellationToken: cancellationToken);
    }
}
