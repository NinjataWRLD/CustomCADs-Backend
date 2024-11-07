namespace CustomCADs.Shared.Core.Events.Email;

public record PasswordResetRequestedEvent(string Email, string Endpoint) : IEvent;
