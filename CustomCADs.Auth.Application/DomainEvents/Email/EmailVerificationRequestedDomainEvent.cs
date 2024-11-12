using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record EmailVerificationRequestedDomainEvent(
    string Email,
    string Endpoint
) : BaseDomainEvent;
