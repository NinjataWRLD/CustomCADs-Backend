using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.Application.Events;

public interface IEventRaiser
{
    public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
}
