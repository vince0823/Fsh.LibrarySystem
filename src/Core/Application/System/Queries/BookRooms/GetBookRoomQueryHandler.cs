using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Application.System.Departments;
using FSH.Learn.Domain.System;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookRooms;
public class GetBookRoomQueryHandler : IRequestHandler<GetBookRoomQuery, BookRoomDto>
{
    private readonly IReadRepository<BookRoom> _repository;
    private readonly IUserService _userService;

    public GetBookRoomQueryHandler(IReadRepository<BookRoom> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<BookRoomDto> Handle(GetBookRoomQuery request, CancellationToken cancellationToken)
    {
        var bookRoom = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        _ = bookRoom ?? throw new NotFoundException("书屋不存在");
        var dto = bookRoom.Adapt<BookRoomDto>();
        dto.DutyUserName = (await _userService.GetAsync(dto.DutyUserId, cancellationToken: cancellationToken))?.UserName;
        return dto;

    }
}
