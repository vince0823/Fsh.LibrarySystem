using FluentValidation;
using FSH.Learn.Application.System.Departments;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class CreatMenuRequestValidator : CustomValidator<CreatMenuRequest>
{
    public CreatMenuRequestValidator(IReadRepository<Menu> meunRepo, IStringLocalizer<CreatMenuRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await meunRepo.GetBySpecAsync(new MenuByNameSpec(name), ct) is null)
                .WithMessage((_, name) => string.Format(localizer["menu.alreadyexists"], name));
        RuleFor(p => p.Url)
            .NotEmpty().WithMessage("请输入菜单地址");
        RuleFor(p => p.DisplayName)
         .NotEmpty().WithMessage("请输入别名");
        RuleFor(p => p.Icon)
           .NotEmpty().WithMessage("请输入菜单图标");
        RuleFor(p => p.Order).NotNull().MustAsync(async (order, ct) => await IsNumber(order, ct)).WithMessage("请输入正确的排序");
    }

    private Task<bool> IsNumber(int order, CancellationToken cancellationToken)
    {
        string ss = order.ToString();

        var zz = Regex.IsMatch(ss, @"^[1-9]\d*|0$");
        return Task.FromResult(Regex.IsMatch(ss, @"^[1-9]\d*|0$"));
    }

}
