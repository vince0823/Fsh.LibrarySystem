using FSH.Learn.Application.System.Commands.BookShelfLayers;
using FSH.Learn.Application.System.Queries.BookShelfLayers;

namespace FSH.Learn.Host.Controllers.System;

public class BookShelfLayerController : VersionedApiController
{

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.BookShelfLayers)]
    [OpenApiOperation("Search BookShelfLayers using available filters.", "")]
    public Task<PaginationResponse<BookShelfLayerDto>> SearchAsync(GetBookShelfLayerListQuery query)
    {
        return Mediator.Send(query);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.BookShelfLayers)]
    [OpenApiOperation("Create a new BookShelfLayer.", "")]
    public Task<Guid> CreateAsync(CreateBookShelfLayerCommand command)
    {
        return Mediator.Send(command);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.BookShelfLayers)]
    [OpenApiOperation("Get BookShelfLayer details.", "")]
    public Task<BookShelfLayerDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBookShelfLayerQuery(id));
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.BookShelfLayers)]
    [OpenApiOperation("Update a BookShelfLayer.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBookShelfLayerCommand command, Guid id)
    {
        return id != command.Id
            ? BadRequest()
            : Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.BookShelfLayers)]
    [OpenApiOperation("Delete a BookShelfLayer.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBookShelfLayerCommand(id));
    }

    /// <summary>
    /// 书屋-书架-层数.
    /// </summary>
    /// <returns></returns>
    [HttpGet("tree")]
    [MustHavePermission(FSHAction.Tree, FSHResource.BookShelfLayers)]
    [OpenApiOperation("Get BookShelfLayerTree.", "")]
    public Task<List<BookShelfLayerTreeDto>> GetBookShelfLayerTreeAsync()
    {
        return Mediator.Send(new GetBookShelfLayerTreeQuery());
    }
}
