using FSH.Learn.Application.Common.Events;
using FSH.Learn.Domain.Common.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Catalog.Products.EventHandlers;
internal class InvalidateProductDeleteHandler : IEventNotificationHandler<ProductDeleteEvent>
{
    private readonly ISender _sender;
    public InvalidateProductDeleteHandler(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// 此处的Handle 也可以不发送command  直接在此处用处理请求.
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(EventNotification<ProductDeleteEvent> notification, CancellationToken cancellationToken)
    {
        var command = new DeleteProductRequest(
               notification.Event.ProductId);
        await _sender.Send(command, cancellationToken);
    }
}
