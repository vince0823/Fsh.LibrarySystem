using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Application.System.Departments;
using FSH.Learn.Domain.System;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class GetMenutRequest : IRequest<MenuDetailsDto>
{
    public Guid Id { get; set; }

    public GetMenutRequest(Guid id) => Id = id;
}

public class GetMenutRequestHandler : IRequestHandler<GetMenutRequest, MenuDetailsDto>
{
    private readonly IRepository<Menu> _repository;
    private readonly IStringLocalizer<GetMenutRequestHandler> _localizer;

    public GetMenutRequestHandler(IRepository<Menu> repository, IStringLocalizer<GetMenutRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MenuDetailsDto> Handle(GetMenutRequest request, CancellationToken cancellationToken)
    {
        var menu = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        _ = menu ?? throw new NotFoundException(string.Format(_localizer["entity.notfound"], "菜单", request.Id));
        var dto = menu.Adapt<MenuDetailsDto>();
        if (menu.ParentId != Guid.Empty)
        {
            var parentMenu = _repository.GetByIdAsync(menu.ParentId);
            dto.ParentName = parentMenu.Result?.Name;
        }

        return dto;
    }
}