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
public class CreatMenuRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Url { get; set; }
    public Guid? ParentId { get; set; }
    public string Icon { get; set; }
    public int Order { get; set; }
}

public class CreatMenuRequestRequesHandler : IRequestHandler<CreatMenuRequest, Guid>
{
    private readonly IRepository<Menu> _repository;
    public CreatMenuRequestRequesHandler(IRepository<Menu> repository) =>
       _repository = repository;
    public async Task<Guid> Handle(CreatMenuRequest request, CancellationToken cancellationToken)
    {

        if (request.ParentId is not null)
        {
            var parentMenu = _repository.GetByIdAsync(request.ParentId);
            if (parentMenu == null)
            {
                throw new NotFoundException("parentDepartment Not Found.");
            }
        }

        var menu = new Menu(request.Name, request.Url, request.DisplayName, request.ParentId, request.Icon, request.Order);

        // Add Domain Events to be raised after the commit
        menu.DomainEvents.Add(EntityCreatedEvent.WithEntity(menu));
        await _repository.AddAsync(menu, cancellationToken);
        return menu.Id;
    }
}
