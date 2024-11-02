namespace CustomCADs.Shared.Core.Events;

public record PasswordResetRequestedEvent(string Email, string Endpoint);
