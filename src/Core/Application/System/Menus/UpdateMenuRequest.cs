using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Application.System.Departments;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class UpdateMenuRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Url { get; set; }
    public Guid? ParentId { get; set; }
    public string? Icon { get; set; }
    public int Order { get; set; }

}

public class UpdateMenuRequestHandler : IRequestHandler<UpdateMenuRequest, Guid>
{
    private readonly IRepository<Menu> _repository;
    private readonly IStringLocalizer<UpdateMenuRequestHandler> _localizer;

    public UpdateMenuRequestHandler(IRepository<Menu> repository,  IStringLocalizer<UpdateMenuRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateMenuRequest request, CancellationToken cancellationToken)
    {
        var menu = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = menu ?? throw new NotFoundException(string.Format(_localizer["menu.notfound"], request.Id));
        if (request.ParentId is not null)
        {
            var parentDepartment = _repository.GetByIdAsync(request.ParentId);
            if (parentDepartment == null)
            {
                throw new NotFoundException(string.Format(_localizer["parentDepartment.notfound"], request.Id));
            }
        }

        var updatedMenu = menu.Update(request.Name, request.Url, request.ParentId, request.Icon,request.Order);

        // Add Domain Events to be raised after the commit
        menu.DomainEvents.Add(EntityUpdatedEvent.WithEntity(menu));
        await _repository.UpdateAsync(updatedMenu, cancellationToken);
        return request.Id;
    }
}
