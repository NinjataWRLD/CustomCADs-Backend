using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record EmailVerificationRequestedDomainEvent(
    string Email,
    string Endpoint
) : DomainEvent;
