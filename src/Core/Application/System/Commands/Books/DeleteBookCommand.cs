using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class DeleteBookCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteBookCommand(Guid id)
    {
        Id = id;
    }
}
