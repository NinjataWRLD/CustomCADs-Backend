namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetTimeZoneByIdQuery(AccountId Id) : IQuery<string>;
