using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSH.Learn.Application.System.Commands.BookRooms;
using FSH.Learn.Application.System.Queries.BookRooms;

namespace FSH.Learn.Host.Controllers.System;

public class BookRoomController : VersionedApiController
{

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.BookRooms)]
    [OpenApiOperation("Search BookRooms using available filters.", "")]
    public Task<PaginationResponse<BookRoomDto>> SearchAsync(GetBookRoomListQuery query)
    {
        return Mediator.Send(query);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.BookRooms)]
    [OpenApiOperation("Create a new bookRoom.", "")]
    public Task<Guid> CreateAsync(CreateBookRoomCommand command)
    {
        return Mediator.Send(command);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.BookRooms)]
    [OpenApiOperation("Get bookRoom details.", "")]
    public Task<BookRoomDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBookRoomQuery(id));
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.BookRooms)]
    [OpenApiOperation("Update a bookRoom.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBookRoomCommand command, Guid id)
    {
        return id != command.Id
            ? BadRequest()
            : Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.BookRooms)]
    [OpenApiOperation("Delete a bookRoom.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBookRoomCommand(id));
    }

}
