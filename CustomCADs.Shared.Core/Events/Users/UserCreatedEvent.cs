namespace CustomCADs.Shared.Core.Events;

public record UserCreatedEvent(
    string Role, 
    string Username, 
    string Email, 
    string Password
);
