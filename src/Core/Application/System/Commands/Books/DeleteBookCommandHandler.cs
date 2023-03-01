using FSH.Learn.Application.System.Commands.BookShelfLayers;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Guid>
{
    private readonly IRepositoryWithEvents<Book> _repository;
    public DeleteBookCommandHandler(IRepositoryWithEvents<Book> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(command.Id, cancellationToken);
        _ = book ?? throw new NotFoundException("书籍不存在");

        // Add Domain Events to be raised after the commit
        //bookShelfLayer.DomainEvents.Add(EntityDeletedEvent.WithEntity(bookShelfLayer));
        await _repository.DeleteAsync(book, cancellationToken);
        return book.Id;
    }
}

