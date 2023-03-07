using FSH.Learn.Application.Catalog.Brands;
using FSH.Learn.Application.Services.ImportSheet;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.Common.Events;
using FSH.Learn.Domain.System;
using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class ImportBooksFromSheetCommandHandler : IRequestHandler<ImportBooksFromSheetCommand, ImportSheetResultDto>
{
    private readonly IImportService _importService;
    private readonly IRepository<Book> _repository;
    private readonly IRepository<BookShelfLayer> _layerRepository;

    public ImportBooksFromSheetCommandHandler(IImportService importService, IRepository<Book> repository, IRepository<BookShelfLayer> layerRepository)
    {
        _importService = importService;
        _repository = repository;
        _layerRepository = layerRepository;
    }

    public async Task<ImportSheetResultDto> Handle(ImportBooksFromSheetCommand request, CancellationToken cancellationToken)
    {
        var rows = request.Rows;

        var errorInfos = await _importService.TryReadValuesAsync<ImportBookDto>(rows, async (dto, _, __) =>
        {
            if (!Enum.IsDefined(typeof(BookType), dto.BookType))
            {
                throw new ExceptionUtil("类型不存在");
            }

            var layer = await _layerRepository.GetBySpecAsync(new BookShelfLayerByLayerNameSpec(dto.BookShelfLayerName), cancellationToken);
            if (layer == null)
            {
                throw new ExceptionUtil("书籍所属位置不存在");
            }

            var book = await _repository.GetBySpecAsync(new BookByNameSpec(dto.Name, dto.Author), cancellationToken);

            bool isTrasient = book is null;
            if (isTrasient)
            {
                book = new Book(dto.Name, dto.Author, dto.BookType, layer.Id, dto.Description);
                book.DomainEvents.Add(EntityCreatedEvent.WithEntity(book));
                await _repository.AddAsync(book, cancellationToken);
            }
            else
            {
                book.Update(dto.Name, dto.Author, dto.BookType, layer.Id, dto.Description);
                book.DomainEvents.Add(EntityUpdatedEvent.WithEntity(book));
                await _repository.UpdateAsync(book, cancellationToken);
            }
        }, cancellationToken: cancellationToken);

        return new ImportSheetResultDto
        {
            Total = rows.Count,
            Errors = errorInfos
        };
    }
}
