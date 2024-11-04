namespace CustomCADs.Shared.Core.Events;

public record UserRegisteredEvent(
    string Role, 
    string Username, 
    string Email, 
    string? FirstName = default, 
    string? LastName = default
);
