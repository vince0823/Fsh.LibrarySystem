using FSH.Learn.Application.System.Commands.BookShelfs;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfLayers;
public class CreateBookShelfLayerCommandHandler : IRequestHandler<CreateBookShelfLayerCommand, Guid>
{
    private readonly IRepository<BookShelfLayer> _repository;
    public CreateBookShelfLayerCommandHandler(IRepository<BookShelfLayer> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateBookShelfLayerCommand request, CancellationToken cancellationToken)
    {
        var bookShelfLayer = new BookShelfLayer(request.LayerName, request.BookShelfId);
        bookShelfLayer.DomainEvents.Add(EntityCreatedEvent.WithEntity(bookShelfLayer));
        await _repository.AddAsync(bookShelfLayer, cancellationToken);
        return bookShelfLayer.Id;
    }
}

