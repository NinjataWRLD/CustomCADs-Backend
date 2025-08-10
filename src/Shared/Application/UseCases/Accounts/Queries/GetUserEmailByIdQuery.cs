namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetUserEmailByIdQuery(
	AccountId Id
) : IQuery<string>;
