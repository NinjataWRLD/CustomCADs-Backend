namespace CustomCADs.Accounts.Endpoints.Common.Dtos;

public sealed record AccountResponse(
    string Username,
    string Email,
    string Role,
    string? FirstName,
    string? LastName
);
