namespace CustomCADs.Shared.Core.Events;

public interface IEventRaiser
{
    public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
}
