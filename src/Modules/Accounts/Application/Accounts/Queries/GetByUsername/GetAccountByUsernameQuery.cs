namespace CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;

public sealed record GetAccountByUsernameQuery(
    string Username
) : IQuery<GetAccountByUsernameDto>;