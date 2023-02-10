using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookRooms;
public class UpdateBookRoomCommand : IRequest<Guid>
{

    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? DutyUserId { get; set; }
}

public class UpdateBookRoomCommandValidator : CustomValidator<UpdateBookRoomCommand>
{
    public UpdateBookRoomCommandValidator()
    {
        RuleFor(p => p.Name)
           .NotEmpty()
               .WithMessage("请填写书屋名称");
        RuleFor(p => p.Address).NotEmpty().WithMessage("请填写书屋地址");
    }
}
