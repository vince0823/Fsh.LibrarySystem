using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.Learn.Application.Services.ImportSheet;
using FSH.Learn.Domain.Common.Contracts;
using FSH.Learn.Domain.Common.Events;

namespace FSH.Learn.Application.Catalog.Brands;
internal class ImportBrandsFromSheetCommandHandler : IRequestHandler<ImportBrandsFromSheetCommand, ImportSheetResultDto>
{

    private readonly IImportService _importService;
    private readonly IRepository<Brand> _repository;

    public ImportBrandsFromSheetCommandHandler(IImportService importService, IRepository<Brand> repository)
    {
        _importService = importService;
        _repository = repository;

    }

    public async Task<ImportSheetResultDto> Handle(ImportBrandsFromSheetCommand request, CancellationToken cancellationToken)
    {
        var rows = request.Rows;

        var errorInfos = await _importService.TryReadValuesAsync<ImportBrandDto>(rows, async (dto, _, __) =>
        {
            var brand = await _repository.GetBySpecAsync(new BrandByNameSpec(dto.Name), cancellationToken);

            bool isTrasient = brand is null;
            if (isTrasient)
            {
                brand = new Brand(dto.Name, dto.Description);
                brand.DomainEvents.Add(EntityCreatedEvent.WithEntity(brand));
                await _repository.AddAsync(brand, cancellationToken);
            }
            else
            {
                brand.Update(dto.Name, dto.Description);
                brand.DomainEvents.Add(EntityUpdatedEvent.WithEntity(brand));
                await _repository.UpdateAsync(brand, cancellationToken);
            }
        }, cancellationToken: cancellationToken);

        return new ImportSheetResultDto
        {
            Total = rows.Count,
            Errors = errorInfos
        };
    }
}
