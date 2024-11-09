using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account;

public record RoleCreatedEvent(string Name, string Description) : IntegrationEvent;
