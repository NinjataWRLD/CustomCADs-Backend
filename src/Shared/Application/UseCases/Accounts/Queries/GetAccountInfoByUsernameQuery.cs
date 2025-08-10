namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public record GetAccountInfoByUsernameQuery(
	string Username
) : IQuery<AccountInfo>;

public record AccountInfo(
	DateTimeOffset CreatedAt,
	bool TrackViewedProducts,
	string? FirstName,
	string? LastName
);
