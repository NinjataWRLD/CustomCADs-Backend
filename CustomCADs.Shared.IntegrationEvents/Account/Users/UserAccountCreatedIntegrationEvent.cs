using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserAccountCreatedIntegrationEvent(
    UserId Id,
    string Role,
    string Username,
    string Email
) : BaseIntegrationEvent;
