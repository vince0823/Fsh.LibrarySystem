using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSH.Learn.Application.Services.ImportSheet;

namespace FSH.Learn.Application.Catalog.Brands;
internal class ImportBrandsFromSheetCommandHandler : IRequestHandler<ImportBrandsFromSheetCommand, ImportSheetResultDto>
{

    private readonly IImportService _importService;
    private readonly IRepository<Brand> _brandRepo;
    public ImportBrandsFromSheetCommandHandler(IImportService importService, IRepository<Brand> brandRepo)
    {
        _importService = importService;
        _brandRepo = brandRepo;
    }

    public Task<ImportSheetResultDto> Handle(ImportBrandsFromSheetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
