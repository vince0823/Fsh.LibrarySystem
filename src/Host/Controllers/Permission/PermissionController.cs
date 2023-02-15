using FSH.Learn.Application.Dashboard;
using FSH.Learn.Application.System.Queries.BookShelfs;
using FSH.Learn.Application.System.Queries.Permissions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.Learn.Host.Controllers.Permission;

public class PermissionController : VersionedApiController
{
    [AllowAnonymous]
    [HttpGet("list")]
    [OpenApiOperation("permission list.", "")]
    public Task<List<PermissionDto>> SearchAsync()
    {
        return Mediator.Send(new GetPermissionListQuery());
    }
}
