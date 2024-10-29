namespace CustomCADs.Shared.Events.Events;

public record PasswordResetRequestedEvent(string Email, string Endpoint);
