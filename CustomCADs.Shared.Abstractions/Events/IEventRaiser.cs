using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Shared.Abstractions.Events;

public interface IEventRaiser
{
    public Task RaiseDomainEventAsync<TEvent>(TEvent @event) where TEvent : BaseDomainEvent;
    public Task RaiseIntegrationEventAsync<TEvent>(TEvent @event) where TEvent : BaseIntegrationEvent;
}
