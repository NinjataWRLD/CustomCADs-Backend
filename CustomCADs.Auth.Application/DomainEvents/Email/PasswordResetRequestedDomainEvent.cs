using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record PasswordResetRequestedDomainEvent(
    string Email,
    string Endpoint
) : BaseDomainEvent;
