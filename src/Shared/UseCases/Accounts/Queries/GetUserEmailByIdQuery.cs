namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public sealed record GetUserEmailByIdQuery(
	AccountId Id
) : IQuery<string>;
