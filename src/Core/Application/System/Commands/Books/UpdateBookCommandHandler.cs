using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Guid>
{
    private IRepositoryWithEvents<Book> _repository;
    public UpdateBookCommandHandler(IRepositoryWithEvents<Book> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(command.Id, cancellationToken);
        _ = book ?? throw new NotFoundException("书籍不存在");
        var updatedbook = book.Update(command.Name, command.Author, command.BookType, command.BookShelfLayerId, command.Description);
        await _repository.UpdateAsync(updatedbook, cancellationToken);
        return book.Id;
    }
}
