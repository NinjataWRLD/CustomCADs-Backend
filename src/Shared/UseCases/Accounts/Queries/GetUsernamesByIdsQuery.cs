namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public sealed record GetUsernamesByIdsQuery(
     AccountId[] Ids
) : IQuery<Dictionary<AccountId, string>>;
