using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfs;
public class UpdateBookShelfCommandValidator : CustomValidator<UpdateBookShelfCommand>
{
    public UpdateBookShelfCommandValidator(IReadRepository<BookShelf> repository, IReadRepository<BookRoom> bookRoomRepository)
    {
        RuleFor(p => p.Code)
           .NotEmpty()
           .MaximumLength(75)
           .MustAsync(async (command, code, ct) => await repository.GetBySpecAsync(new BookShelfByCodeSpec(code, command.BookRoomId, command.Id), ct) is null)
               .WithMessage("书架编号已存在");
        RuleFor(p => p.BookRoomId).NotEmpty().MustAsync(async (bookRoomId, ct) => await bookRoomRepository.GetByIdAsync(bookRoomId, ct) is not null)
            .WithMessage("书屋不存在");
    }

}
