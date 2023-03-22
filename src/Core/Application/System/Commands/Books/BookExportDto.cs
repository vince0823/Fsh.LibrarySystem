using FSH.Learn.Domain.System.EnumExt;
using FSH.Learn.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class BookExportDto
{

    public string Name { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string BookType { get; set; }
    public string BookShelfLayerName{ get; set; } = default!;
    public string? Description { get; set; }
}
