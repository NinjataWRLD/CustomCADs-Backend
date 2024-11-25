using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserDeletedIntegrationEvent(
    string Username
) : BaseIntegrationEvent;
