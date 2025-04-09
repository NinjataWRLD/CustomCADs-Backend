using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Identity.Domain.Users.Events;

public record PasswordResetRequestedDomainEvent(
    string Email,
    string Endpoint
) : BaseDomainEvent;
