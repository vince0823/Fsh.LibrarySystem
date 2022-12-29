using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class CreateDepartmentRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public string? DutyUserId { get; set; }
}

public class CreateDepartmentRequesHandler : IRequestHandler<CreateDepartmentRequest, Guid>
{
    private readonly IRepository<Department> _repository;
    private readonly IUserService _userService;
    public CreateDepartmentRequesHandler(IRepository<Department> repository, IUserService userService) =>
       (_repository, _userService) = (repository, userService);
    public async Task<Guid> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
    {

        if (request.ParentId is not null)
        {
            var parentDepartment = _repository.GetByIdAsync(request.ParentId);
            if (parentDepartment == null)
            {
                throw new NotFoundException("parentDepartment Not Found.");
            }
        }

        if (!string.IsNullOrEmpty(request.DutyUserId))
        {
            var user = await _userService.GetAsync(request.DutyUserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User Not Found.");
            }
        }

        var department = new Department(request.Name, request.Description, request.ParentId, request.DutyUserId);

        // Add Domain Events to be raised after the commit
        department.DomainEvents.Add(EntityCreatedEvent.WithEntity(department));

        await _repository.AddAsync(department, cancellationToken);

        return department.Id;
    }
}

