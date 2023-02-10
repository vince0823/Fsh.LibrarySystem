using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookRooms;
public class DeleteBookRoomCommand : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteBookRoomCommand(Guid id) => Id = id;
}
