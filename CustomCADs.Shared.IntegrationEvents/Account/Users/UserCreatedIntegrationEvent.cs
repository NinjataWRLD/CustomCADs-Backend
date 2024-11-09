using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserCreatedIntegrationEvent(
    Guid Id,
    string Role,
    string Username,
    string Email,
    string Password
) : IntegrationEvent;
