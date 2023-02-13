using FSH.Learn.Application.System.Commands.BookShelfs;
using FSH.Learn.Application.System.Queries.BookShelfs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.Learn.Host.Controllers.System;

public class BookShelfController : VersionedApiController
{

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.BookShelfs)]
    [OpenApiOperation("Search BookShelfs using available filters.", "")]
    public Task<PaginationResponse<BookShelfDto>> SearchAsync(GetBookShelfListQuery query)
    {
        return Mediator.Send(query);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.BookShelfs)]
    [OpenApiOperation("Create a new BookShelf.", "")]
    public Task<Guid> CreateAsync(CreateBookShelfCommand command)
    {
        return Mediator.Send(command);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.BookShelfs)]
    [OpenApiOperation("Get BookShelf details.", "")]
    public Task<BookShelfDetailDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBookShelfQuery(id));
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.BookShelfs)]
    [OpenApiOperation("Update a BookShelf.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBookShelfCommand command, Guid id)
    {
        return id != command.Id
            ? BadRequest()
            : Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.BookShelfs)]
    [OpenApiOperation("Delete a BookShelf.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBookShelfCommand(id));
    }
}
