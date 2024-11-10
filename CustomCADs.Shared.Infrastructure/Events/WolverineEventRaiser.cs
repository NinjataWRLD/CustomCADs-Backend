using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.Infrastructure.Events;

public class WolverineEventRaiser(Wolverine.IMessageBus bus) : IEventRaiser
{
    public async Task RaiseAsync<TEvent>(TEvent @event) where TEvent : BaseEvent
        => await bus.PublishAsync(@event);
}
