using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfs;
public class DeleteBookShelfCommand : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteBookShelfCommand(Guid id) => Id = id;
}

