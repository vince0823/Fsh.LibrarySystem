using FSH.Learn.Application.Catalog.Brands;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookRooms;
public class CreateBookRoomCommand : IRequest<Guid>
{
    /// <summary>
    /// 书屋名称.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// 地址.
    /// </summary>
    public string Address { get; set; } = default!;

    /// <summary>
    /// 书屋负责人.
    /// </summary>
    public string? DutyUserId { get; set; }
}

public class CreateBookRoomCommandValidator : CustomValidator<CreateBookRoomCommand>
{
    public CreateBookRoomCommandValidator(IReadRepository<BookRoom> repository, IStringLocalizer<CreateBookRoomCommandValidator> localizer)
    {
        RuleFor(p => p.Name)
           .NotEmpty()
           .MaximumLength(75)
           .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new BookRoomByNameSpec(name), ct) is null)
               .WithMessage((_, name) => string.Format(localizer["bookroom.alreadyexists"], name));
        RuleFor(p => p.Address).NotEmpty().WithMessage("请填写书屋地址");
    }
}