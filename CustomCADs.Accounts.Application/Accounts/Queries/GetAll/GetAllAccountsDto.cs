namespace CustomCADs.Accounts.Application.Accounts.Queries.GetAll;

public record GetAllAccountsDto(
    AccountId Id,
    string Username,
    string Email,
    string Role,
    string? FirstName = null,
    string? LastName = null
);
