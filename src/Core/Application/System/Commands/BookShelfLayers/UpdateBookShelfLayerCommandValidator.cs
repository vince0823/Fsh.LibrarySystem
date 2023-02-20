using FSH.Learn.Application.Catalog.Brands;
using FSH.Learn.Application.System.Commands.BookShelfs;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfLayers;
public class UpdateBookShelfLayerCommandValidator : CustomValidator<UpdateBookShelfLayerCommand>
{
    public UpdateBookShelfLayerCommandValidator(IReadRepository<BookShelfLayer> repository, IReadRepository<BookShelf> bookShelfRepository)
    {
        RuleFor(p => p.LayerName)
           .NotEmpty()
           .MaximumLength(75)
            //.MustAsync(async (command, layerName, ct) => await repository.GetBySpecAsync(new BookShelfLayerByLayerNameSpec(layerName, command.BookShelfId, command.Id), ct) is null)
            //    .WithMessage("书架层名称已存在");
            .MustAsync(async (command, name, ct) =>
                    await repository.GetBySpecAsync(new BookShelfLayerByLayerNameSpec(name, command.BookShelfId), ct)
                        is not BookShelfLayer existingBookShelfLayer || existingBookShelfLayer.Id == command.Id)
                .WithMessage("书架层名称已存在"); // MustAsync  必须符合某个条件
        RuleFor(p => p.BookShelfId).NotEmpty().MustAsync(async (bookRoomId, ct) => await bookShelfRepository.GetByIdAsync(bookRoomId, ct) is not null)
            .WithMessage("书架不存在");
    }

}

