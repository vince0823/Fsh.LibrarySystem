using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Application.System.Departments;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookRooms;
public class GetBookRoomListQueryHandler : IRequestHandler<GetBookRoomListQuery, PaginationResponse<BookRoomDto>>
{

    private readonly IReadRepository<BookRoom> _repository;
    private readonly IUserService _userService;

    public GetBookRoomListQueryHandler(IReadRepository<BookRoom> repository, IUserService userService) => (_repository, _userService) = (repository, userService);

    public async Task<PaginationResponse<BookRoomDto>> Handle(GetBookRoomListQuery query, CancellationToken cancellationToken)
    {
        var spec = new BookRoomBySearchRequestSpec(query);
        var list = await _repository.PaginatedListAsync(spec, query.PageNumber, query.PageSize, cancellationToken: cancellationToken);
        if (list.Data.Count > 0)
        {
            list.Data.ForEach(async v => v.DutyUserName = (await _userService.GetAsync(v.DutyUserId, cancellationToken))?.UserName);
        }

        return list;

    }
}
