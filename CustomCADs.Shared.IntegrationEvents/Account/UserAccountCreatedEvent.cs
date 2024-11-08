using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Shared.IntegrationEvents.Account;

public record UserAccountCreatedEvent(
    Guid Id,
    string Role,
    string Username,
    string Email
) : IEvent;
