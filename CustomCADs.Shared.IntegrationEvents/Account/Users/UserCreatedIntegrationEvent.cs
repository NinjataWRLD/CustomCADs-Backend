using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserCreatedIntegrationEvent(
    UserId Id,
    string Role,
    string Username,
    string Email,
    string Password
) : BaseIntegrationEvent;
