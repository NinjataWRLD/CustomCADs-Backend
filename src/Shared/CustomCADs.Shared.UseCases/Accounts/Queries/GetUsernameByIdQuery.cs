namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public sealed record GetUsernameByIdQuery(
    AccountId Id
) : IQuery<string>;
