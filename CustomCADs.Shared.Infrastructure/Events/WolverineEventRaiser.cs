using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Common.Events;
using Wolverine;

namespace CustomCADs.Shared.Infrastructure.Events;

public class WolverineEventRaiser(IMessageBus bus) : IEventRaiser
{
    public async Task RaiseAsync<TEvent>(TEvent @event) where TEvent : BaseEvent
        => await bus.PublishAsync(@event);
}
