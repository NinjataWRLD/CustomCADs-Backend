namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public sealed record GetTimeZoneByIdQuery(
    AccountId Id
) : IQuery<string>;
