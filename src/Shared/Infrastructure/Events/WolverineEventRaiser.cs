using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Domain.Bases.Events;
using Wolverine;

namespace CustomCADs.Shared.Infrastructure.Events;

public sealed class WolverineEventRaiser(IMessageBus bus) : IEventRaiser
{
	public async Task RaiseApplicationEventAsync<TEvent>(TEvent @event) where TEvent : BaseApplicationEvent
		=> await bus.PublishAsync(@event).ConfigureAwait(false);
}
