using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserDeletedIntegrationEvent(
    string Username
) : BaseIntegrationEvent;
