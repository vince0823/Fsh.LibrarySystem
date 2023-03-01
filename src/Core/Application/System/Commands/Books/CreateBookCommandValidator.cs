﻿using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class CreateBookCommandValidator : CustomValidator<CreateBookCommand>
{
    public CreateBookCommandValidator(IReadRepository<Book> repository, IReadRepository<BookShelfLayer> bookShelfLayerRepository)
    {
        RuleFor(p => p.Name)
           .NotEmpty()
           .MaximumLength(75).WithMessage("书名不为空");

        RuleFor(p => p.Author)
          .NotEmpty()
          .MaximumLength(75)
          .MustAsync(async (command, author, ct) => await repository.GetBySpecAsync(new BookByNameSpec(command.Name, author), ct) is null)// MustAsync()添加必须满足  否则报错
              .WithMessage("该作者已有相同名称的书籍");
        RuleFor(p => p.BookType)
         .NotEmpty()
         .Must(bookType => Enum.IsDefined(typeof(BookType), bookType)).WithMessage("不存在该书籍类型");

        RuleFor(p => p.BookShelfLayerId).NotEmpty().MustAsync(async (bookShelfLayerId, ct) => await bookShelfLayerRepository.GetByIdAsync(bookShelfLayerId, ct) is not null)
            .WithMessage("书架层不存在");
    }
}
