using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private IRepositoryWithEvents<Book> _repository;
    public CreateBookCommandHandler(IRepositoryWithEvents<Book> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var book = new Book(command.Name, command.Author, command.BookType, command.BookShelfLayerId, command.Description);
        await _repository.AddAsync(book, cancellationToken);
        return book.Id;
    }
}
