using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Departments;
public class UpdateDepartmentRequestValidator : CustomValidator<UpdateDepartmentRequest>
{
    public UpdateDepartmentRequestValidator(IReadRepository<Department> departmentRepo, IStringLocalizer<UpdateDepartmentRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (department, name, ct) =>
                    await departmentRepo.GetBySpecAsync(new DepartmentByNameSpec(name), ct)
                        is not Department existingDepartment || existingDepartment.Id == department.Id)
                .WithMessage((_, name) => string.Format(localizer["department.alreadyexists"], name));
    }
}
