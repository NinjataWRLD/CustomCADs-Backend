using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Identity.Domain.Users.Events;

public record EmailVerificationRequestedDomainEvent(
    string Email,
    string Endpoint
) : BaseDomainEvent;
