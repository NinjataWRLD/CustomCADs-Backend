using CustomCADs.Shared.Core.Events;
using Wolverine;

namespace CustomCADs.Shared.Infrastructure.Events;

public class WolverineEventRaiser(IMessageBus bus) : IEventRaiser
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : Core.Events.IEvent
        => await bus.PublishAsync(@event);
}
