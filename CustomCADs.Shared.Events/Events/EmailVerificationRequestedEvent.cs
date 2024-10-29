namespace CustomCADs.Shared.Events.Events;

public record EmailVerificationRequestedEvent(string Email, string Endpoint);
