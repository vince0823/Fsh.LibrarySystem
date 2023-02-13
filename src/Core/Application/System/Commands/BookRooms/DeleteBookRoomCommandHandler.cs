using FSH.Learn.Application.System.Departments;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookRooms;
public class DeleteBookRoomCommandHandler : IRequestHandler<DeleteBookRoomCommand, Guid>
{
    private readonly IRepository<BookRoom> _repository;
    public DeleteBookRoomCommandHandler(IRepository<BookRoom> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteBookRoomCommand request, CancellationToken cancellationToken)
    {
        var bookRoom = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = bookRoom ?? throw new NotFoundException("书屋不存在");

        // Add Domain Events to be raised after the commit
        bookRoom.DomainEvents.Add(EntityDeletedEvent.WithEntity(bookRoom));

        await _repository.DeleteAsync(bookRoom, cancellationToken);

        return request.Id;
    }
}
