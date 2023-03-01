using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Application.System.Services;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using FSH.Learn.Domain.System.EnumExt;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.Books;
internal class GetBookListQueryHandler : IRequestHandler<GetBookListQuery, PaginationResponse<BookDto>>
{

    private readonly IReadRepository<Book> _bookRepo;
    private readonly IBookShelfLayerService _bookShelfLayerService;
    private readonly IUserService _userService;

    public GetBookListQueryHandler(IReadRepository<Book> bookRepo, IBookShelfLayerService bookShelfLayerService, IUserService userService)
    {
        _bookRepo = bookRepo;
        _bookShelfLayerService = bookShelfLayerService;
        _userService = userService;
    }

    public async Task<PaginationResponse<BookDto>> Handle(GetBookListQuery query, CancellationToken cancellationToken)
    {
        var spec = new BookBySearchSpec(query);
        var dtolist = await _bookRepo.PaginatedListAsync(spec, query.PageNumber, query.PageSize, cancellationToken: cancellationToken);
        if (dtolist.TotalCount > 0)
        {
            foreach (var item in dtolist.Data)
            {
                item.BookTypeName = EnumManager.GetEnumDisplayName(item.BookType);
                item.CreateUserName = (await _userService.GetAsync(item.CreatedBy, cancellationToken))?.UserName;
                item.Address = await _bookShelfLayerService.GetBookAdress(item.BookShelfLayerId, cancellationToken);
            }
        }

        return dtolist;
    }

}
