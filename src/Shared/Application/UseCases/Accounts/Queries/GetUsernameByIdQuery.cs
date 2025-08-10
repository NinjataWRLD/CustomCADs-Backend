namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetUsernameByIdQuery(
	AccountId Id
) : IQuery<string>;
