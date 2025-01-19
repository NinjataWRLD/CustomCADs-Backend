namespace CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;

public record GetAccountByUsernameDto(
    AccountId Id,
    string Role,
    string Username,
    string Email,
    string? FirstName = null,
    string? LastName = null
);
