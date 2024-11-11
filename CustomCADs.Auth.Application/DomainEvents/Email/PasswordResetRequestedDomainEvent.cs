using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record PasswordResetRequestedDomainEvent(
    string Email,
    string Endpoint
) : BaseDomainEvent;
