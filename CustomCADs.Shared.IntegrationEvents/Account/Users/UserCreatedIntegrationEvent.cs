using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserCreatedIntegrationEvent(
    UserId Id,
    string Role,
    string Username,
    string Email,
    string Password
) : BaseIntegrationEvent;
