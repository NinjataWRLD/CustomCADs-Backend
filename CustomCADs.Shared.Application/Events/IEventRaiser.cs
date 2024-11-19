using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Shared.Application.Events;

public interface IEventRaiser
{
    public Task RaiseDomainEventAsync<TEvent>(TEvent @event) where TEvent : BaseDomainEvent;
    public Task RaiseIntegrationEventAsync<TEvent>(TEvent @event) where TEvent : BaseIntegrationEvent;
}
