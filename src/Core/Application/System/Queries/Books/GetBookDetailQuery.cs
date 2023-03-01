using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.Books;
public class GetBookDetailQuery : IRequest<BookDto>
{
    public Guid Id { get; set; }

    public GetBookDetailQuery(Guid id) => Id = id;
}
