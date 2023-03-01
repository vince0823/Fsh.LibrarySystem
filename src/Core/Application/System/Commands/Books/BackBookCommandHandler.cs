using FSH.Learn.Domain.System;
using FSH.Learn.Domain.System.EnumExt;
using FSH.Learn.Domain.System.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
internal class BackBookCommandHandler : IRequestHandler<BackBookCommand, Guid>
{
    private readonly IRepositoryWithEvents<Book> _repository;
    private readonly IEventPublisher _events;
    public BackBookCommandHandler(IRepositoryWithEvents<Book> repository, IEventPublisher events)
    {
        _repository = repository;
        _events = events;
    }

    public async Task<Guid> Handle(BackBookCommand command, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(command.Id, cancellationToken);
        _ = book ?? throw new NotFoundException("书籍不存在");
        if (!book.IsBorrowed)
        {
            throw new ConflictException("该书本已归还");
        }

        book.Back(false);
        await _repository.UpdateAsync(book, cancellationToken);
        await _events.PublishAsync(new BookRecordAddEvent(book.Id, BookRecordType.Back));
        return book.Id;
    }
}