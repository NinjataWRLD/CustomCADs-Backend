using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account;

public record RoleDeletedEvent(string Name) : IEvent;
