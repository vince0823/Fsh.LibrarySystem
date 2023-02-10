using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookRooms;
public class CreateBookRoomCommandHandler : IRequestHandler<CreateBookRoomCommand, Guid>
{

    private readonly IRepository<BookRoom> _repository;
    private readonly IUserService _userService;

    public CreateBookRoomCommandHandler(IRepository<BookRoom> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Guid> Handle(CreateBookRoomCommand command, CancellationToken cancellationToken)
    {
        if (command.DutyUserId is not null)
        {
            var dutyUser = await _userService.GetAsync(command.DutyUserId, cancellationToken);
            if (dutyUser != null)
            {
                throw new NotFoundException("负责人不存在");
            }
        }

        var bookRoom = new BookRoom(command.Name, command.Address, command.DutyUserId);
        bookRoom.DomainEvents.Add(EntityCreatedEvent.WithEntity(bookRoom));
        await _repository.AddAsync(bookRoom, cancellationToken);
        return bookRoom.Id;
    }
}
