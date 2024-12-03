namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public sealed record GetTimeZonesByIdsQuery(
    AccountId[] Ids
) : IQuery<(AccountId Id, string TimeZone)[]>;
