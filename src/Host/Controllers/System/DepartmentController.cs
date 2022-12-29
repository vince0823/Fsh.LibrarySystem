using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Application.System.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.Learn.Host.Controllers.System;

public class DepartmentController : VersionedApiController
{

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Departments)]
    [OpenApiOperation("Search departments using available filters.", "")]
    public Task<PaginationResponse<DepartmentDto>> SearchAsync(SearchDepartmentsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Departments)]
    [OpenApiOperation("Create a new department.", "")]
    public Task<Guid> CreateAsync(CreateDepartmentRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Departments)]
    [OpenApiOperation("Get department details.", "")]
    public Task<DepartmentDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetDepartmentRequest(id));
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Departments)]
    [OpenApiOperation("Update a department.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateDepartmentRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Departments)]
    [OpenApiOperation("Delete a department.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteDepartmentRequest(id));
    }

    [HttpGet("tree")]
    [MustHavePermission(FSHAction.Tree, FSHResource.Departments)]
    [OpenApiOperation("department tree.", "")]
    public Task<List<DepartmentTreeDto>> TreeAsync()
    {
        return Mediator.Send(new TreeDepartmentRequest());
    }

}
