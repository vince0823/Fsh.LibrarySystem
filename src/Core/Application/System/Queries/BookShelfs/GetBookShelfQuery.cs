using FSH.Learn.Application.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfs;
public class GetBookShelfQuery : IRequest<BookShelfDetailDto>
{
    public Guid Id { get; set; }

    public GetBookShelfQuery(Guid id) => Id = id;
}

