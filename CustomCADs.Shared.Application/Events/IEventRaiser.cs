using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Shared.Application.Events;

public interface IEventRaiser
{
    public Task RaiseAsync<TEvent>(TEvent @event) where TEvent : BaseEvent;
}
