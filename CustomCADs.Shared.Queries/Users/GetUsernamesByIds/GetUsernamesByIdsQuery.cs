using CustomCADs.Shared.Application.Requests.Queries;

namespace CustomCADs.Shared.Queries.Users.GetUsernamesByIds;

public record GetUsernamesByIdsQuery(params UserId[] Ids)
    : IQuery<IEnumerable<(UserId Id, string Username)>>;
