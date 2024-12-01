namespace CustomCADs.Accounts.Endpoints.Accounts;

public record AccountResponse(
    string Username,
    string Email,
    string Role,
    string? FirstName,
    string? LastName
);
