using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.BookShelfs;
public class CreateBookShelfCommand : IRequest<Guid>
{
    /// <summary>
    /// 书架编号.
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// 书屋Id.
    /// </summary>
    public Guid BookRoomId { get; set; }
}
