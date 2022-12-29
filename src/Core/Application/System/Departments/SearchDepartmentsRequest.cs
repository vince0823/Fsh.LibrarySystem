using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class SearchDepartmentsRequest : PaginationFilter, IRequest<PaginationResponse<DepartmentDto>>
{
    public string? DutyUserId { get; set; }
}

public class SearchDepartmentsRequestHandler : IRequestHandler<SearchDepartmentsRequest, PaginationResponse<DepartmentDto>>
{
    private readonly IReadRepository<Department> _repository;
    private readonly IUserService _user;

    public SearchDepartmentsRequestHandler(IReadRepository<Department> repository, IUserService user) => (_repository, _user) = (repository, user);

    public async Task<PaginationResponse<DepartmentDto>> Handle(SearchDepartmentsRequest request, CancellationToken cancellationToken)
    {
        var spec = new DepartmentsBySearchRequestWithBrandsSpec(request);
        var list = await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
        foreach (var item in list.Data)
        {
            if (item.ParentId is not null)
            {
                item.ParentName = (await _repository.GetByIdAsync(item.ParentId, cancellationToken))?.Name;
            }

            if (!string.IsNullOrEmpty(item.DutyUserId))
            {
                item.DutyUserName = (await _user.GetAsync(item.DutyUserId, cancellationToken))?.UserName;
            }
        }

        return list;
    }
}
