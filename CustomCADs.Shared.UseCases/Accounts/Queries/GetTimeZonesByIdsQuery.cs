namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetTimeZonesByIdsQuery(params AccountId[] Ids)
    : IQuery<(AccountId Id, string TimeZone)[]>;
