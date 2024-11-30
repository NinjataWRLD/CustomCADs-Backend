namespace CustomCADs.Accounts.Endpoints.Users;

public record AccountResponse(
    string Username,
    string Email,
    string Role,
    string? FirstName,
    string? LastName
);
