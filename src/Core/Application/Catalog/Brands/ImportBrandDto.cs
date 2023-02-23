using FSH.Learn.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Catalog.Brands;
public class ImportBrandDto
{
    /// <summary>
    /// 名称.
    /// </summary>
    [Required(ErrorMessage = ValidationMessage.FieldIsRequired)]
    [Display(Name = "名称")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// 描述.
    /// </summary>
    [Display(Name = "描述")]
    public string? Description { get; set; }
}
