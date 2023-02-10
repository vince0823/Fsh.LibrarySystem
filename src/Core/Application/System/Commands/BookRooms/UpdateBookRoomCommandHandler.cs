using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Domain.Catalog;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookRooms;
public class UpdateBookRoomCommandHandler : IRequestHandler<UpdateBookRoomCommand, Guid>
{
    private readonly IRepository<BookRoom> _repository;
    private readonly IUserService _userService;

    public UpdateBookRoomCommandHandler(IRepository<BookRoom> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Guid> Handle(UpdateBookRoomCommand request, CancellationToken cancellationToken)
    {
        var bookRoom = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = bookRoom ?? throw new NotFoundException("书屋不存在");
        if(request.DutyUserId is not null)
        {
            var dutyUser = await _userService.GetAsync(request.DutyUserId, cancellationToken);
            _ = dutyUser ?? throw new NotFoundException("负责人不存在");
        }
        var updatedBookRoom = bookRoom.Update(request.Name, request.Address, request.DutyUserId);
        bookRoom.DomainEvents.Add(EntityUpdatedEvent.WithEntity(bookRoom));
        await _repository.UpdateAsync(updatedBookRoom, cancellationToken);
        return bookRoom.Id;
    }
}
