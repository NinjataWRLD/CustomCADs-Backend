namespace CustomCADs.Shared.Core.Events.Email;

public record EmailVerificationRequestedEvent(string Email, string Endpoint) : IEvent;
