using FSH.Learn.Application.System.Queries.BookShelfs;
using FSH.Learn.Application.System.Services;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfLayers;
public class GetBookShelfLayerQueryHandler : IRequestHandler<GetBookShelfLayerQuery, BookShelfLayerDto>
{
    private readonly IRepository<BookShelfLayer> _repository;
    private readonly IBookShelfLayerService _bookShelfLayerService;

    public GetBookShelfLayerQueryHandler(IRepository<BookShelfLayer> repository, IBookShelfLayerService bookShelfLayerService) =>
        (_repository, _bookShelfLayerService) = (repository, bookShelfLayerService);

    public async Task<BookShelfLayerDto> Handle(GetBookShelfLayerQuery query, CancellationToken cancellationToken)
    {
        var dto = await _repository.GetBySpecAsync(
             (ISpecification<BookShelfLayer, BookShelfLayerDto>)new BookShelfLayerByIdWithBookShelfSpec(query.Id), cancellationToken);
        if (dto is null)
        {

            throw new NotFoundException("未找到该书架层");
        }

        dto.Address = await _bookShelfLayerService.GetBookAdress(query.Id, cancellationToken);
        return dto;
    }
}
