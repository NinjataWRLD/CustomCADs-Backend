namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetById;

public record GetAccountByIdDto(
	AccountId Id,
	string Role,
	string Username,
	string Email,
	string? FirstName,
	string? LastName,
	DateTimeOffset CreatedAt
);
