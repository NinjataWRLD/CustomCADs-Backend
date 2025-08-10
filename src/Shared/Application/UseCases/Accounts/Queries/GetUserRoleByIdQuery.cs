namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetUserRoleByIdQuery(
	AccountId Id
) : IQuery<string>;
