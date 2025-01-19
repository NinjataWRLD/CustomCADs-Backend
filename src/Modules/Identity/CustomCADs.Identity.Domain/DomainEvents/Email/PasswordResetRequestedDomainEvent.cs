using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Identity.Domain.DomainEvents.Email;

public record PasswordResetRequestedDomainEvent(
    string Email,
    string Endpoint
) : BaseDomainEvent;
