using FSH.Learn.Application.Catalog.Brands;
using FSH.Learn.Application.Documents;
using FSH.Learn.Application.Services.ImportSheet;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace FSH.Learn.Host.Controllers.Catalog;

public class BrandsController : VersionedApiController
{
    private readonly IExcelService _excelService;
    private readonly IBrandService _brandService;
    public BrandsController(IExcelService excelService, IBrandService brandService)
    {
        _excelService = excelService;
        _brandService = brandService;
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public Task<PaginationResponse<BrandDto>> SearchAsync(SearchBrandsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Get brand details.", "")]
    public Task<BrandDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBrandRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Create a new brand.", "")]
    public Task<Guid> CreateAsync(CreateBrandRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Update a brand.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBrandRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Delete a brand.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBrandRequest(id));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.Brands)]
    [OpenApiOperation("Generate a number of random brands.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandomBrandRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.Brands)]
    [OpenApiOperation("Delete the brands generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> DeleteRandomAsync()
    {
        return Mediator.Send(new DeleteRandomBrandRequest());
    }

    [HttpPost("Sheet")]
    [MustHavePermission(FSHAction.Import, FSHResource.Brands)]
    [OpenApiOperation("ImportSheet brands.", "")]
    public async Task<ImportSheetResultDto> ImportSheetAsync([FromForm] IFormFile file)
    {
        SpreadSheetValidator.EnsureExtensionIsValid(file.FileName);
        await using var stream = file.OpenReadStream();
        var rowValues = _excelService.ReadValues(stream);

        return await Mediator.Send(new ImportBrandsFromSheetCommand(rowValues));
    }


    [HttpPost("Export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Brands)]
    [OpenApiOperation("ExportSheet brands.", "")]
    public async Task<IActionResult> ExportSheetAsync()
    {

        var sources = await _brandService.GetBrands();
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Files/Templates");
        string path = Path.Combine(folder, "brand_form.xlsx");
        var fileStream = new FileStream(path, FileMode.Open);
        var lastStream = _excelService.WriteCollection(fileStream, sources);
        this.HttpContext.Response.Headers.Add("Content-Length", lastStream.Length.ToString());
        this.HttpContext.Response.Headers.Add("Content-Type", "charset=UTF-8");
        return File(lastStream, "application/octet-stream;charset=UTF-8", Path.GetFileName(path));
    }

}