namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetAccountInfoByUsernameQuery(
	string Username
) : IQuery<AccountInfo>;


public record AccountInfo(
	DateTimeOffset CreatedAt,
	string? FirstName,
	string? LastName
);
