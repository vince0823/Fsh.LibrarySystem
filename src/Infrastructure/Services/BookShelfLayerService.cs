using FSH.Learn.Application.Common.Exceptions;
using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.System.Services;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Infrastructure.Services;
public class BookShelfLayerService : IBookShelfLayerService
{
    private readonly IReadRepository<BookRoom> _roomRepository;
    private readonly IReadRepository<BookShelf> _shelfRepository;
    private readonly IReadRepository<BookShelfLayer> _layerRepository;

    public BookShelfLayerService(IReadRepository<BookRoom> roomRepository, IReadRepository<BookShelf> shelfRepository, IReadRepository<BookShelfLayer> layerRepository)
    {
        _roomRepository = roomRepository;
        _shelfRepository = shelfRepository;
        _layerRepository = layerRepository;
    }

    public async Task<string> GetBookAdress(Guid layId, CancellationToken cancellationToken)
    {
        var layer = await _layerRepository.GetByIdAsync(layId, cancellationToken);
        if (layer is null)
        {
            throw new NotFoundException("未找到该书架层");
        }

        var shelf = await _shelfRepository.GetByIdAsync(layer.BookShelfId, cancellationToken);
        if (shelf is null)
        {
            throw new NotFoundException("未找到该书架");
        }

        var room = await _roomRepository.GetByIdAsync(shelf.BookRoomId, cancellationToken);
        if (room is null)
        {
            throw new NotFoundException("未找到该书屋");
        }

        return string.Format("{0}{1}{2}", room.Name, shelf.Code, layer.LayerName);

    }
}
