namespace CustomCADs.Accounts.Application.Accounts.Queries.GetById;

public record GetAccountByIdQuery(AccountId Id) : IQuery<GetAccountByIdDto>;
