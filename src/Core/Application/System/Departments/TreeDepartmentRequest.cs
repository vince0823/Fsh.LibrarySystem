using FSH.Learn.Application.Catalog.Brands;
using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class TreeDepartmentRequest : IRequest<List<DepartmentTreeDto>>
{

}

public class TreeDepartmentRequestHandler : IRequestHandler<TreeDepartmentRequest, List<DepartmentTreeDto>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepository<Department> _departmentRepo;
    private readonly IStringLocalizer<TreeDepartmentRequestHandler> _localizer;

    public TreeDepartmentRequestHandler(IRepository<Department> departmentRepo, IStringLocalizer<TreeDepartmentRequestHandler> localizer) =>
        (_departmentRepo, _localizer) = (departmentRepo, localizer);

    public async Task<List<DepartmentTreeDto>> Handle(TreeDepartmentRequest request, CancellationToken cancellationToken)
    {

        var dtoList = new List<DepartmentTreeDto>();
        var parentDepartmentList = await _departmentRepo.ListAsync(new DepartmentByParentIdNullSpec(), cancellationToken);
        foreach (var parentDepartment in parentDepartmentList)
        {
            dtoList.Add(new DepartmentTreeDto
            {
                Id = parentDepartment.Id,
                Name = parentDepartment.Name,
                ParentId = parentDepartment.ParentId,
                ChildDepartmentList = await GetChildren(parentDepartment.Id)
            });
        }

        return dtoList;
    }

    private async Task<List<ChildDepartment>> GetChildren(Guid pid)
    {
        List<Department> depList = await _departmentRepo.ListAsync(new DepartmentByParentIdSpec(pid));
        List<ChildDepartment> list = new List<ChildDepartment>();
        foreach (Department dep in depList)
        {
            list.Add(new ChildDepartment
            {
                Id = dep.Id,
                Name = dep.Name,
                ParentId = dep.ParentId,
                ChildDepartmentList = await GetChildren(dep.Id)
            });
        }

        return list;
    }
}