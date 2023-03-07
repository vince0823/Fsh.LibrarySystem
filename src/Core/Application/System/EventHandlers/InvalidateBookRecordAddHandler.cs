using FSH.Learn.Application.Catalog.Products;
using FSH.Learn.Application.System.Commands.BookRecords;
using FSH.Learn.Application.System.IServices;
using FSH.Learn.Domain.System.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.EventHandlers;
internal class InvalidateBookRecordAddHandler : IEventNotificationHandler<BookRecordAddEvent>
{
    private readonly ISender _sender;
    private readonly IBookRecordAddService _bookRecordAddService;
    public InvalidateBookRecordAddHandler(ISender sender, IBookRecordAddService bookRecordAddService)
    {
        _sender = sender;
        _bookRecordAddService = bookRecordAddService;
    }

    public async Task Handle(EventNotification<BookRecordAddEvent> notification, CancellationToken cancellationToken)
    {
        var command = new BookRecordAddCommand(
              notification.Event.BookId,
              notification.Event.BookRecordType);
        // await _sender.Send(command, cancellationToken);
        await _bookRecordAddService.BookRecordAdd(command, cancellationToken);
    }
}
