namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetByUsername;

public sealed record GetAccountByUsernameQuery(
	string Username
) : IQuery<GetAccountByUsernameDto>;
