using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account;

public record UserCreatedEvent(
    Guid Id,
    string Role,
    string Username,
    string Email,
    string Password
) : IntegrationEvent;
