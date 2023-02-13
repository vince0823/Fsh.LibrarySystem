using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfs;
public class GetBookShelfQueryHandler : IRequestHandler<GetBookShelfQuery, BookShelfDetailDto>
{
    private readonly IRepository<BookShelf> _repository;
    public GetBookShelfQueryHandler(IRepository<BookShelf> repository) =>
        _repository = repository;

    public async Task<BookShelfDetailDto> Handle(GetBookShelfQuery query, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<BookShelf, BookShelfDetailDto>)new BookShelfByIdWithBookRoomSpec(query.Id), cancellationToken)
        ?? throw new ExceptionUtil("未找到该书架");
}