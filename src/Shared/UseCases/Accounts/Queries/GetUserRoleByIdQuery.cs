namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public sealed record GetUserRoleByIdQuery(
	AccountId Id
) : IQuery<string>;
