using FSH.Learn.Application.System.Departments;
using FSH.Learn.Application.System.Queries.Permissions;
using FSH.Learn.Application.System.Specification;
using FSH.Learn.Domain.System;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Queries.BookShelfLayers;
internal class GetBookShelfLayerTreeQueryHandler : IRequestHandler<GetBookShelfLayerTreeQuery, List<BookShelfLayerTreeDto>>
{
    private readonly IReadRepository<BookRoom> _roomRepository;
    private readonly IReadRepository<BookShelf> _shelfRepository;
    private readonly IReadRepository<BookShelfLayer> _layerRepository;
    public GetBookShelfLayerTreeQueryHandler(IReadRepository<BookRoom> roomRepository, IReadRepository<BookShelf> shelfRepository, IReadRepository<BookShelfLayer> layerRepository)
    {
        _roomRepository = roomRepository;
        _shelfRepository = shelfRepository;
        _layerRepository = layerRepository;
    }

    public async Task<List<BookShelfLayerTreeDto>> Handle(GetBookShelfLayerTreeQuery request, CancellationToken cancellationToken)
    {
        List<BookShelfLayerTreeDto> dtoList = new List<BookShelfLayerTreeDto>();

        // 获取书屋.
        var roomList = await _roomRepository.ListAsync(cancellationToken);
        if (roomList.Count > 0)
        {
            foreach (var room in roomList)
            {
                dtoList.Add(new BookShelfLayerTreeDto
                {
                    Id = room.Id,
                    Name = room.Name,
                    Children = await GetShelfChildren(room.Id, cancellationToken),

                });

            }
        }

        return dtoList;
    }

    private async Task<List<BookShelfLayerTreeDto>> GetShelfChildren(Guid roomId, CancellationToken cancellationToken)
    {
        List<BookShelf> shelfList = await _shelfRepository.ListAsync(new BookShelfByRoomIdSpec(roomId), cancellationToken);
        List<BookShelfLayerTreeDto> dtoList = new List<BookShelfLayerTreeDto>();
        foreach (BookShelf shelf in shelfList)
        {
            dtoList.Add(new BookShelfLayerTreeDto
            {
                Id = shelf.Id,
                Name = shelf.Code,
                Children = await GetLayerChildren(shelf.Id, cancellationToken)
            });
        }

        return dtoList;
    }

    private async Task<List<BookShelfLayerTreeDto>> GetLayerChildren(Guid shelfId, CancellationToken cancellationToken)
    {
        List<BookShelfLayer> shelflayerList = await _layerRepository.ListAsync(new BookShelfLayerByShilfIdSpec(shelfId), cancellationToken);
        List<BookShelfLayerTreeDto> dtoList = new List<BookShelfLayerTreeDto>();
        foreach (BookShelfLayer layer in shelflayerList)
        {
            dtoList.Add(new BookShelfLayerTreeDto
            {
                Id = layer.Id,
                Name = layer.LayerName,
                Children = new List<BookShelfLayerTreeDto>()
            });
        }

        return dtoList;
    }
}
