using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class UpdateDepartmentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public string? DutyUserId { get; set; }
}

public class UpdateDepartmentRequestHandler : IRequestHandler<UpdateDepartmentRequest, Guid>
{
    private readonly IRepository<Department> _repository;
    private readonly IUserService _userService;
    private readonly IStringLocalizer<UpdateDepartmentRequestHandler> _localizer;

    public UpdateDepartmentRequestHandler(IRepository<Department> repository, IUserService userService, IStringLocalizer<UpdateDepartmentRequestHandler> localizer) =>
        (_repository, _userService, _localizer) = (repository, userService, localizer);

    public async Task<Guid> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
    {
        var department = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = department ?? throw new NotFoundException(string.Format(_localizer["department.notfound"], request.Id));
        if (request.ParentId is not null)
        {
            var parentDepartment = _repository.GetByIdAsync(request.ParentId);
            if (parentDepartment == null)
            {
                throw new NotFoundException(string.Format(_localizer["parentDepartment.notfound"], request.Id));
            }
        }

        if (!string.IsNullOrEmpty(request.DutyUserId))
        {
            var user = await _userService.GetAsync(request.DutyUserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(string.Format(_localizer["user.notfound"], request.DutyUserId));
            }
        }

        var updatedDepartment = department.Update(request.Name, request.Description, request.ParentId, request.DutyUserId);

        // Add Domain Events to be raised after the commit
        department.DomainEvents.Add(EntityUpdatedEvent.WithEntity(department));
        await _repository.UpdateAsync(updatedDepartment, cancellationToken);
        return request.Id;
    }
}
