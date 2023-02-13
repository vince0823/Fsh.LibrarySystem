using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfs;
public class UpdateBookShelfCommand : IRequest<Guid>
{

    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public Guid BookRoomId { get; set; }
}
