namespace CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;

public record GetAccountByUsernameQuery(string Username) : IQuery<GetAccountByUsernameDto>;