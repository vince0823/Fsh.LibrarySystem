using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class UpdateBookCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Author { get; set; } = default!;
    public BookType BookType { get; set; }
    public Guid BookShelfLayerId { get; set; }
    public string? Description { get; set; }
}
