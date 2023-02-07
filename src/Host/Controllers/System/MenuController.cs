using FSH.Learn.Application.System.Departments;
using FSH.Learn.Application.System.Menus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.Learn.Host.Controllers.System;

public class MenuController : VersionedApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Menus)]
    [OpenApiOperation("Create a new ment.", "")]
    public Task<Guid> CreateAsync(CreatMenuRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Menus)]
    [OpenApiOperation("Get ment details.", "")]
    public Task<MenuDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetMenutRequest(id));
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Menus)]
    [OpenApiOperation("Update a menu.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateMenuRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Menus)]
    [OpenApiOperation("Delete a menu.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteMenuRequest(id));
    }

    [HttpGet("tree")]
    [MustHavePermission(FSHAction.Tree, FSHResource.Menus)]
    [OpenApiOperation("menu tree.", "")]
    public Task<List<MenuTreeDto>> TreeAsync()
    {
        return Mediator.Send(new TreeMenuRequest());
    }
}
