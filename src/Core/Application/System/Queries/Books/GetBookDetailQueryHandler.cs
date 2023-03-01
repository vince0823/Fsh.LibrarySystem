using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Application.System.Queries.BookShelfLayers;
using FSH.Learn.Application.System.Services;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using FSH.Learn.Domain.System.EnumExt;
using Mapster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.Books;
public class GetBookDetailQueryHandler : IRequestHandler<GetBookDetailQuery, BookDto>
{
    private readonly IReadRepository<Book> _bookRepo;
    private readonly IBookShelfLayerService _bookShelfLayerService;
    private readonly IUserService _userService;
    public GetBookDetailQueryHandler(IReadRepository<Book> bookRepo, IBookShelfLayerService bookShelfLayerService, IUserService userService)
    {
        _bookRepo = bookRepo;
        _bookShelfLayerService = bookShelfLayerService;
        _userService = userService;
    }

    public async Task<BookDto> Handle(GetBookDetailQuery query, CancellationToken cancellationToken)
    {
        var selectBook = await _bookRepo.GetByIdAsync(query.Id, cancellationToken);
        if (selectBook is null)
        {
            throw new NotFoundException("未找到该书籍");
        }

        var dto = selectBook.Adapt<BookDto>();
        dto.BookTypeName = EnumManager.GetEnumDisplayName(dto.BookType);
        dto.CreateUserName = (await _userService.GetAsync(dto.CreatedBy, cancellationToken))?.UserName;
        dto.Address = await _bookShelfLayerService.GetBookAdress(dto.BookShelfLayerId, cancellationToken);
        return dto;
    }
}
