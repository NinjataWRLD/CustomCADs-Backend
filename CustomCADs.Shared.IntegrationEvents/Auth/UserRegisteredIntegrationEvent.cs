using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Shared.IntegrationEvents.Auth;

public record UserRegisteredIntegrationEvent(
    string Role,
    string Username,
    string Email,
    string? FirstName = default,
    string? LastName = default
) : BaseIntegrationEvent;
