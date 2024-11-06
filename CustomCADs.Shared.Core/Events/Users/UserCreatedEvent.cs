namespace CustomCADs.Shared.Core.Events;

public record UserCreatedEvent(
    Guid Id, 
    string Role, 
    string Username, 
    string Email, 
    string Password
);
