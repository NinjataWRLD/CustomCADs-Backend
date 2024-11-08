using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.Infrastructure.Events;

public class WolverineEventRaiser(Wolverine.IMessageBus bus) : IEventRaiser
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        => await bus.PublishAsync(@event);
}
