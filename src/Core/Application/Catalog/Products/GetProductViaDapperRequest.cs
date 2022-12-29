using Mapster;

namespace FSH.Learn.Application.Catalog.Products;

public class GetProductViaDapperRequest : IRequest<ProductDto>
{
    public Guid Id { get; set; }

    public GetProductViaDapperRequest(Guid id) => Id = id;
}

public class GetProductViaDapperRequestHandler : IRequestHandler<GetProductViaDapperRequest, ProductDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<GetProductViaDapperRequestHandler> _localizer;
    private readonly IRepository<Brand> _brandRepository;

    public GetProductViaDapperRequestHandler(IDapperRepository repository, IRepository<Brand> brandRepository, IStringLocalizer<GetProductViaDapperRequestHandler> localizer) =>
        (_repository, _brandRepository, _localizer) = (repository, brandRepository,localizer);

    public async Task<ProductDto> Handle(GetProductViaDapperRequest request, CancellationToken cancellationToken)
    {

        var product = await _repository.QueryFirstOrDefaultAsync<Product>(
            $"SELECT * FROM [preReleaseTest].[Catalog].[Products] WHERE Id  = '{request.Id}'", cancellationToken: cancellationToken);

        _ = product ?? throw new NotFoundException(string.Format(_localizer["product.notfound"], request.Id));

        product.Brand = await _brandRepository.GetByIdAsync(product.BrandId, cancellationToken);

        return product.Adapt<ProductDto>();
    }
}