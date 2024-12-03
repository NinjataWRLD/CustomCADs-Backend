namespace CustomCADs.Accounts.Endpoints.Helpers.Dtos;

public sealed record AccountResponse(
    string Username,
    string Email,
    string Role,
    string? FirstName,
    string? LastName
);
