using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserDeletedIntegrationEvent(
    string Username
) : IntegrationEvent;
