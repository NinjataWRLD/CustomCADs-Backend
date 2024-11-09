using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record PasswordResetRequestedDomainEvent(
    string Email, 
    string Endpoint
) : DomainEvent;
