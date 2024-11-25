using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Shared.UseCases.Users.Queries;

public record GetUsernamesByIdsQuery(params UserId[] Ids)
    : IQuery<IEnumerable<(UserId Id, string Username)>>;
