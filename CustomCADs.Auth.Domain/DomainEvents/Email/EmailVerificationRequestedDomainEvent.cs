using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Auth.Domain.DomainEvents.Email;

public record EmailVerificationRequestedDomainEvent(
    string Email,
    string Endpoint
) : BaseDomainEvent;
