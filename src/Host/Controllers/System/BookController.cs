using FSH.Learn.Application.Documents;
using FSH.Learn.Application.Services.ImportSheet;
using FSH.Learn.Application.System.Commands.Books;
using FSH.Learn.Application.System.Queries.Books;
namespace FSH.Learn.Host.Controllers.System;

public class BookController : VersionedApiController
{

    private readonly IExcelService _excelService;
    public BookController(IExcelService excelService)
    {
        _excelService = excelService;
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Book)]
    [OpenApiOperation("Search Book using available filters.", "")]
    public Task<PaginationResponse<BookDto>> SearchAsync(GetBookListQuery query)
    {
        return Mediator.Send(query);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Book)]
    [OpenApiOperation("Create a Book ", "")]
    public Task<Guid> CreateAsync(CreateBookCommand command)
    {
        return Mediator.Send(command);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Book)]
    [OpenApiOperation("Get Book details.", "")]
    public Task<BookDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBookDetailQuery(id));
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Book)]
    [OpenApiOperation("Update a Book.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBookCommand command, Guid id)
    {
        return id != command.Id
            ? BadRequest()
            : Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Book)]
    [OpenApiOperation("Delete a Book.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBookCommand(id));
    }

    [HttpPut("Borrowed")]
    [MustHavePermission(FSHAction.Borrowed, FSHResource.Book)]
    [OpenApiOperation("Borrowed a Book.", "")]
    public Task<Guid> BorrowedAsync(Guid id)
    {
        return Mediator.Send(new BorrowedBookCommand(id));
    }

    [HttpPut("Back")]
    [MustHavePermission(FSHAction.Back, FSHResource.Book)]
    [OpenApiOperation("Back a Book.", "")]
    public Task<Guid> BackAsync(Guid id)
    {
        return Mediator.Send(new BackBookCommand(id));
    }

    [HttpPost("Sheet")]
    [MustHavePermission(FSHAction.Import, FSHResource.Brands)]
    [OpenApiOperation("ImportSheet brands.", "")]
    public async Task<ImportSheetResultDto> ImportSheetAsync([FromForm] IFormFile file)
    {
        SpreadSheetValidator.EnsureExtensionIsValid(file.FileName);
        await using var stream = file.OpenReadStream();
        var rowValues = _excelService.ReadValues(stream);

        return await Mediator.Send(new ImportBooksFromSheetCommand(rowValues));
    }

    //[HttpPost("Export")]
    //[MustHavePermission(FSHAction.Export, FSHResource.Brands)]
    //[OpenApiOperation("ExportSheet brands.", "")]
    //public async Task<IActionResult> ExportSheetAsync()
    //{

    //    var sources = await _brandService.GetBrands();
    //    string folder = Path.Combine(Directory.GetCurrentDirectory(), "Files/Templates");
    //    string path = Path.Combine(folder, "brand_form.xlsx");
    //    var fileStream = new FileStream(path, FileMode.Open);
    //    var lastStream = _excelService.WriteCollection(fileStream, sources);
    //    this.HttpContext.Response.Headers.Add("Content-Length", lastStream.Length.ToString());
    //    this.HttpContext.Response.Headers.Add("Content-Type", "charset=UTF-8");
    //    return File(lastStream, "application/octet-stream;charset=UTF-8", Path.GetFileName(path));
    //}
}
