using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Shared.Queries.Users.GetUsernamesByIds;

public record GetUsernamesByIdsQuery(params UserId[] Ids)
    : IQuery<IEnumerable<(UserId Id, string Username)>>;
