using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Common.Events;
using Wolverine;

namespace CustomCADs.Shared.Infrastructure.Events;

public class WolverineEventRaiser(IMessageBus bus) : IEventRaiser
{
    public async Task RaiseDomainEventAsync<TEvent>(TEvent @event) where TEvent : BaseDomainEvent
        => await bus.PublishAsync(@event);

    public async Task RaiseIntegrationEventAsync<TEvent>(TEvent @event) where TEvent : BaseIntegrationEvent
        => await bus.PublishAsync(@event);
}
