using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.Books;
public class BookDto : IDto
{

    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Author { get; set; }

    public BookType BookType { get; set; }
    public string? BookTypeName { get; set; }
    public bool IsBorrowed { get; set; }
    public Guid BookShelfLayerId { get; set; }
    public string? Address { get; set; }
    public string CreatedBy { get; set; }
    public string? CreateUserName { get; set; }
    public string? Description { get; set;}
}
