namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public sealed record GetUsernamesByIdsQuery(
     AccountId[] Ids
) : IQuery<IEnumerable<(AccountId Id, string Username)>>;
