using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.Identity;
using FSH.Learn.Shared.Events;
using System.Data;

namespace FSH.Learn.Application.Catalog.Products;

public class CreateProductRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public Guid BrandId { get; set; }
    public FileUploadRequest? Image { get; set; }
}

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Guid>
{
    private readonly IRepository<Product> _repository;
    private readonly IFileStorageService _file;
    private readonly IEventPublisher _events;

    public CreateProductRequestHandler(IRepository<Product> repository, IFileStorageService file, IEventPublisher @event) =>
        (_repository, _file, _events) = (repository, file, @event);

    public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        string productImagePath = await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken);

        var product = new Product(request.Name, request.Description, request.Rate, request.BrandId, productImagePath);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityCreatedEvent.WithEntity(product));

        await _repository.AddAsync(product, cancellationToken);

        // ≤‚ ‘EventBus
       //await _events.PublishAsync(new ProductDeleteEvent(product.Id));
        return product.Id;
    }
}