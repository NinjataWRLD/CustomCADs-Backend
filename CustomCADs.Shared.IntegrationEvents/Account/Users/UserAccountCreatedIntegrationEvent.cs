using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserAccountCreatedIntegrationEvent(
    UserId Id,
    string Role,
    string Username,
    string Email
) : BaseIntegrationEvent;
