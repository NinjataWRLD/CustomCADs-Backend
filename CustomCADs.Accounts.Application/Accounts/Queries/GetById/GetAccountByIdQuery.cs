namespace CustomCADs.Accounts.Application.Accounts.Queries.GetById;

public sealed record GetAccountByIdQuery(
    AccountId Id
) : IQuery<GetAccountByIdDto>;
