using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Application.System.Commands.BookShelfs;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfLayers;
public class UpdateBookShelfLayerCommandHandler : IRequestHandler<UpdateBookShelfLayerCommand, Guid>
{
    private readonly IRepository<BookShelfLayer> _repository;
    public UpdateBookShelfLayerCommandHandler(IRepository<BookShelfLayer> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateBookShelfLayerCommand command, CancellationToken cancellationToken)
    {
        var bookShelfLayer = await _repository.GetByIdAsync(command.Id, cancellationToken);
        _ = bookShelfLayer ?? throw new NotFoundException("书架层不存在");
        var updatedBookShelfLayer = bookShelfLayer.Update(command.LayerName, command.BookShelfId);
        bookShelfLayer.DomainEvents.Add(EntityUpdatedEvent.WithEntity(bookShelfLayer));
        await _repository.UpdateAsync(updatedBookShelfLayer, cancellationToken);
        return bookShelfLayer.Id;
    }
}