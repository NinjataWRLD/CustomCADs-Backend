using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserAccountCreatedIntegrationEvent(
    Guid Id,
    string Role,
    string Username,
    string Email
) : IntegrationEvent;
