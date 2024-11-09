using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.IntegrationEvents.Auth;

public record UserRegisteredEvent(
    string Role,
    string Username,
    string Email,
    string? FirstName = default,
    string? LastName = default
) : IntegrationEvent;
