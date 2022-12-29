using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class DeleteDepartmentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteDepartmentRequest(Guid id) => Id = id;
}

public class DeleteDepartmentRequestHandler : IRequestHandler<DeleteDepartmentRequest, Guid>
{
    private readonly IRepository<Department> _repository;
    private readonly IStringLocalizer<DeleteDepartmentRequestHandler> _localizer;

    public DeleteDepartmentRequestHandler(IRepository<Department> repository, IStringLocalizer<DeleteDepartmentRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteDepartmentRequest request, CancellationToken cancellationToken)
    {
        // 多选删除
        // var IdList= new List<Guid>();
        // IdList.Add(request.Id);
        // var departmentList = await _repository.ListAsync(new DeleteDepartmentByIDSpec(IdList), cancellationToken);
        // IEnumerable<Department> coordinates = departmentList.AsEnumerable<Department>();
        // await _repository.DeleteRangeAsync(coordinates, cancellationToken);

        var department = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = department ?? throw new NotFoundException(_localizer["product.notfound"]);

        // Add Domain Events to be raised after the commit
        department.DomainEvents.Add(EntityDeletedEvent.WithEntity(department));

        await _repository.DeleteAsync(department, cancellationToken);

        return request.Id;
    }
}