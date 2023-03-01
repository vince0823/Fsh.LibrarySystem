using FSH.Learn.Application.System.Commands.Books;
using FSH.Learn.Application.System.Commands.BookShelfLayers;
using FSH.Learn.Application.System.Queries.Books;
using FSH.Learn.Application.System.Queries.BookShelfLayers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.Learn.Host.Controllers.System;

public class BookController : VersionedApiController
{
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
}
