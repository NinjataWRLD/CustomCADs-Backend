namespace CustomCADs.Shared.Core.Events.Users;

public record UserCreatedEvent(
    Guid Id,
    string Role,
    string Username,
    string Email,
    string Password
) : IEvent;
