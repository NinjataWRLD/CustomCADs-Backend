namespace CustomCADs.Shared.Application.Abstractions.Events;

public interface IEventRaiser
{
	public Task RaiseApplicationEventAsync<TEvent>(TEvent @event) where TEvent : BaseApplicationEvent;
}
