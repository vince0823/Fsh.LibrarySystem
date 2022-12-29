using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Domain.System;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class GetDepartmentRequest : IRequest<DepartmentDetailsDto>
{
    public Guid Id { get; set; }

    public GetDepartmentRequest(Guid id) => Id = id;
}

public class GetDepartmentRequestHandler : IRequestHandler<GetDepartmentRequest, DepartmentDetailsDto>
{
    private readonly IRepository<Department> _repository;
    private readonly IUserService _userService;
    private readonly IStringLocalizer<GetDepartmentRequestHandler> _localizer;

    public GetDepartmentRequestHandler(IRepository<Department> repository, IUserService userService, IStringLocalizer<GetDepartmentRequestHandler> localizer) =>
        (_repository, _userService, _localizer) = (repository, userService, localizer);

    public async Task<DepartmentDetailsDto> Handle(GetDepartmentRequest request, CancellationToken cancellationToken)
    {
        var department = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        _ = department ?? throw new NotFoundException(string.Format(_localizer["entity.notfound"], "部门", request.Id));
        var dto = department.Adapt<DepartmentDetailsDto>();
        if (department.ParentId != Guid.Empty)
        {
            var parentDepartment = _repository.GetByIdAsync(department.ParentId);
            dto.ParentName = parentDepartment.Result?.Name;
        }

        if (!string.IsNullOrEmpty(department.DutyUserId))
        {
            var user = await _userService.GetAsync(department.DutyUserId, cancellationToken);
            dto.DutyUserName = user?.UserName;
        }

        return dto;
    }
}