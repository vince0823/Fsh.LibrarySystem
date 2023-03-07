using FSH.Learn.Domain.System.EnumExt;
using FSH.Learn.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class ImportBookDto
{
    [Required(ErrorMessage = ValidationMessage.FieldIsRequired)]
    [Display(Name = "名称")]
    public string Name { get; set; } = default!;
    [Required(ErrorMessage = ValidationMessage.FieldIsRequired)]
    [Display(Name = "作者")]
    public string Author { get; set; } = default!;
    [Required(ErrorMessage = ValidationMessage.FieldIsRequired)]
    [Display(Name = "类型")]
    public BookType BookType { get; set; }
    [Required(ErrorMessage = ValidationMessage.FieldIsRequired)]
    [Display(Name = "所属位置")]
    public string BookShelfLayerName { get; set; } = default!;
    [Display(Name = "描述")]
    public string? Description { get; set; }
}
