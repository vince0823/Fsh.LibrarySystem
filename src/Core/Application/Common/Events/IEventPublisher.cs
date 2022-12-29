using FSH.Learn.Shared.Events;

namespace FSH.Learn.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}