using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class BorrowedBookCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public BorrowedBookCommand(Guid id)
    {
        Id = id;
    }
}
