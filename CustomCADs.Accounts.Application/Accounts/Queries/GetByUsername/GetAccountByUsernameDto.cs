namespace CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;

public record GetAccountByUsernameDto(
    AccountId Id,
    string Role,
    string Email,
    string? FirstName = null,
    string? LastName = null
);
