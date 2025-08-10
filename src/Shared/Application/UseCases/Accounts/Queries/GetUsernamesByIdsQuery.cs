namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public sealed record GetUsernamesByIdsQuery(
	 AccountId[] Ids
) : IQuery<Dictionary<AccountId, string>>;
