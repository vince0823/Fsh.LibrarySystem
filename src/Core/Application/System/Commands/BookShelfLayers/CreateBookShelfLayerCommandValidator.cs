using FSH.Learn.Application.System.Commands.BookShelfs;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfLayers;
public class CreateBookShelfLayerCommandValidator : CustomValidator<CreateBookShelfLayerCommand>
{
    public CreateBookShelfLayerCommandValidator(IReadRepository<BookShelfLayer> repository, IReadRepository<BookShelf> bookShelfRepository)
    {
        RuleFor(p => p.LayerName)
           .NotEmpty()
           .MaximumLength(75)
           .MustAsync(async (command, layerName, ct) => await repository.GetBySpecAsync(new BookShelfLayerByLayerNameSpec(layerName, command.BookShelfId), ct) is null)
               .WithMessage("书架层名称已存在");
        RuleFor(p => p.BookShelfId).NotEmpty().MustAsync(async (bookShelfId, ct) => await bookShelfRepository.GetByIdAsync(bookShelfId, ct) is not null)
            .WithMessage("书架不存在");
    }

}

