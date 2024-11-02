namespace CustomCADs.Shared.Core.Events;

public record EmailVerificationRequestedEvent(string Email, string Endpoint);
