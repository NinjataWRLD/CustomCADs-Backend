using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record EmailVerificationRequestedDomainEvent(
    string Email,
    string Endpoint
) : DomainEvent;
