namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetUsernamesByIdsQuery(params AccountId[] Ids)
    : IQuery<IEnumerable<(AccountId Id, string Username)>>;
