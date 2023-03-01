using FSH.Learn.Application.Dashboard;
using FSH.Learn.Application.EnumUtil.Queries;
using FSH.Learn.Domain.System.EnumExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.Learn.Host.Controllers.Enum;

public class EnumController : VersionedApiController
{
    [HttpGet]
    [AllowAnonymous]
    [OpenApiOperation("Get enumvaluelist", "")]
    public Task<List<EnumNode>> GetAsync(string enumNme)
    {
        return Mediator.Send(new GetEnumValueQuery(enumNme));
    }
}
