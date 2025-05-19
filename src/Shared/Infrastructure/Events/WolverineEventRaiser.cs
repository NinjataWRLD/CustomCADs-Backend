using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Bases.Events;
using Wolverine;

namespace CustomCADs.Shared.Infrastructure.Events;

public sealed class WolverineEventRaiser(IMessageBus bus) : IEventRaiser
{
	public async Task RaiseDomainEventAsync<TEvent>(TEvent @event) where TEvent : BaseDomainEvent
		=> await bus.PublishAsync(@event).ConfigureAwait(false);

	public async Task RaiseApplicationEventAsync<TEvent>(TEvent @event) where TEvent : BaseApplicationEvent
		=> await bus.PublishAsync(@event).ConfigureAwait(false);
}
