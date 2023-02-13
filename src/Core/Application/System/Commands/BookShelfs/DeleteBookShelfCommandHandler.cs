using FSH.Learn.Application.System.Commands.BookRooms;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfs;
public class DeleteBookShelfCommandHandler : IRequestHandler<DeleteBookShelfCommand, Guid>
{
    private readonly IRepository<BookShelf> _repository;
    public DeleteBookShelfCommandHandler(IRepository<BookShelf> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteBookShelfCommand command, CancellationToken cancellationToken)
    {
        var bookShelf = await _repository.GetByIdAsync(command.Id, cancellationToken);
        _ = bookShelf ?? throw new NotFoundException("书架不存在");

        // Add Domain Events to be raised after the commit
        bookShelf.DomainEvents.Add(EntityDeletedEvent.WithEntity(bookShelf));
        await _repository.DeleteAsync(bookShelf, cancellationToken);
        return bookShelf.Id;
    }
}
