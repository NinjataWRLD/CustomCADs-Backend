using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record PasswordResetRequestedEvent(
    string Email, 
    string Endpoint
) : IEvent;
