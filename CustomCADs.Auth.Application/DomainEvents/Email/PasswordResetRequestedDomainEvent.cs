using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record PasswordResetRequestedDomainEvent(
    string Email,
    string Endpoint
) : BaseDomainEvent;
