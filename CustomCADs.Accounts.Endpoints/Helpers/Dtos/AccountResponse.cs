namespace CustomCADs.Accounts.Endpoints.Helpers.Dtos;

public record AccountResponse(
    string Username,
    string Email,
    string Role,
    string? FirstName,
    string? LastName
);
