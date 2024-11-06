namespace CustomCADs.Shared.Core.Events.Users;

public record UserAccountCreatedEvent(
    Guid Id, 
    string Role, 
    string Username, 
    string Email
);
