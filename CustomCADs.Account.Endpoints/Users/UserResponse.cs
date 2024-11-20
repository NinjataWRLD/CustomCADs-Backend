namespace CustomCADs.Account.Endpoints.Users;

public record UserResponse(
    string Username,
    string Email,
    string Role,
    string? FirstName,
    string? LastName
);
