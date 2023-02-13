using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Application.System.Commands.BookRooms;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfs;
public class CreateBookShelfCommandHandler : IRequestHandler<CreateBookShelfCommand, Guid>
{
    private readonly IRepository<BookShelf> _repository;
    public CreateBookShelfCommandHandler(IRepository<BookShelf> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateBookShelfCommand request, CancellationToken cancellationToken)
    {
        var bookShelf = new BookShelf(request.Code, request.BookRoomId);
        bookShelf.DomainEvents.Add(EntityCreatedEvent.WithEntity(bookShelf));
        await _repository.AddAsync(bookShelf, cancellationToken);
        return bookShelf.Id;
    }
}
