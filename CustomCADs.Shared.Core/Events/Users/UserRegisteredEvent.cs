namespace CustomCADs.Shared.Core.Events.Users;

public record UserRegisteredEvent(
    string Role,
    string Username,
    string Email,
    string? FirstName = default,
    string? LastName = default
);
