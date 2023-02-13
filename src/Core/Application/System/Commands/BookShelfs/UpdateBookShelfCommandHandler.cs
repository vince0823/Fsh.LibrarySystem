using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Application.System.Commands.BookShelfs;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfs;
public class UpdateBookShelfCommandHandler : IRequestHandler<UpdateBookShelfCommand, Guid>
{
    private readonly IRepository<BookShelf> _repository;
    private readonly IUserService _userService;

    public UpdateBookShelfCommandHandler(IRepository<BookShelf> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Guid> Handle(UpdateBookShelfCommand command, CancellationToken cancellationToken)
    {
        var bookShelf = await _repository.GetByIdAsync(command.Id, cancellationToken);
        _ = bookShelf ?? throw new NotFoundException("书架不存在");
        var updatedBookShelf = bookShelf.Update(command.Code, command.BookRoomId);
        bookShelf.DomainEvents.Add(EntityUpdatedEvent.WithEntity(bookShelf));
        await _repository.UpdateAsync(updatedBookShelf, cancellationToken);
        return bookShelf.Id;
    }
}