using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.System.Commands.BookShelfs;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfLayers;
public class DeleteBookShelfLayerCommandHandler : IRequestHandler<DeleteBookShelfLayerCommand, Guid>
{
    private readonly IRepository<BookShelfLayer> _repository;
    public DeleteBookShelfLayerCommandHandler(IRepository<BookShelfLayer> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteBookShelfLayerCommand command, CancellationToken cancellationToken)
    {
        var bookShelfLayer = await _repository.GetByIdAsync(command.Id, cancellationToken);
        _ = bookShelfLayer ?? throw new NotFoundException("书架层不存在");

        // Add Domain Events to be raised after the commit
        bookShelfLayer.DomainEvents.Add(EntityDeletedEvent.WithEntity(bookShelfLayer));
        await _repository.DeleteAsync(bookShelfLayer, cancellationToken);
        return bookShelfLayer.Id;
    }
}
