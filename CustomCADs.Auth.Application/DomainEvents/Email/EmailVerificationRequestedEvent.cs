using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Auth.Application.DomainEvents.Email;

public record EmailVerificationRequestedEvent(
    string Email, 
    string Endpoint
) : IEvent;
