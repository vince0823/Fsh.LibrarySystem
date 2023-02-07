using FSH.Learn.Application.System.Departments;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class DeleteMenuRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteMenuRequest(Guid id) => Id = id;
}

public class DeleteMenuRequestHandler : IRequestHandler<DeleteMenuRequest, Guid>
{
    private readonly IRepository<Menu> _repository;
    private readonly IStringLocalizer<DeleteMenuRequestHandler> _localizer;

    public DeleteMenuRequestHandler(IRepository<Menu> repository, IStringLocalizer<DeleteMenuRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteMenuRequest request, CancellationToken cancellationToken)
    {
        var menu = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = menu ?? throw new NotFoundException(string.Format(_localizer["meun.notfound"],request.Id));

        // Add Domain Events to be raised after the commit
        menu.DomainEvents.Add(EntityDeletedEvent.WithEntity(menu));
        await _repository.DeleteAsync(menu, cancellationToken);
        return request.Id;
    }
}
