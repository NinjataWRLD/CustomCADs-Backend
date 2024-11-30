namespace CustomCADs.Accounts.Application.Accounts.Queries.GetById;

public record GetAccountByIdDto(
    string Role,
    string Username,
    string Email,
    string? FirstName,
    string? LastName
);
