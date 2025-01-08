namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public sealed record GetTimeZonesByIdsQuery(
    AccountId[] Ids
) : IQuery<Dictionary<AccountId, string>>;
