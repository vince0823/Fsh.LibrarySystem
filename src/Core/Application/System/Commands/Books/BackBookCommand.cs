using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class BackBookCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public BackBookCommand(Guid id)
    {
        Id = id;
    }
}