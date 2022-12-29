using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Application.Identity.Users;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class CreateDepartmentRequestValidator : CustomValidator<CreateDepartmentRequest>
{
    public CreateDepartmentRequestValidator(IReadRepository<Department> departmentRepo,  IStringLocalizer<CreateDepartmentRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await departmentRepo.GetBySpecAsync(new DepartmentByNameSpec(name), ct) is null)
                .WithMessage((_, name) => string.Format(localizer["department.alreadyexists"], name));

    }
}
