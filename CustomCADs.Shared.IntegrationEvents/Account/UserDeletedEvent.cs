using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account;

public record UserDeletedEvent(string Username) : IntegrationEvent;
