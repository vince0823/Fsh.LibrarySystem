using Ardalis.Specification;
using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.System.Commands.Books;
using FSH.Learn.Application.System.IServices;
using FSH.Learn.Application.System.Services;
using FSH.Learn.Domain.System;
using FSH.Learn.Domain.System.EnumExt;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSH.Learn.Infrastructure.Services;
public class BookService : IBookService
{

    public readonly IRepository<Book> _repository;
    public readonly IBookShelfLayerService _bookShelfLayerService;
    public BookService(IRepository<Book> repository, IBookShelfLayerService bookShelfLayerService)
    {
        _repository = repository;
        _bookShelfLayerService = bookShelfLayerService;
    }

    public async Task<List<BookExportDto>> GetBooks(CancellationToken cancellationToken)
    {
        var list = await _repository.ListAsync(cancellationToken);

        var books = new List<BookExportDto>();

        foreach (var item in list)
        {
            books.Add(new BookExportDto
            {
                Name = item.Name,
                Author = item.Author,
                BookType = EnumManager.GetEnumDisplayName(item.BookType),
                BookShelfLayerName = await _bookShelfLayerService.GetBookAdress(item.BookShelfLayerId, cancellationToken),
                Description = item.Description,

            });
        }

        return books;
    }
}
