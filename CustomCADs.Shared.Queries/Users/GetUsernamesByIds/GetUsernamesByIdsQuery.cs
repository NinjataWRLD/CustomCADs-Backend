using CustomCADs.Shared.Application.Requests.Queries;

namespace CustomCADs.Shared.Queries.Users.GetUsernamesByIds;

public record GetUsernamesByIdsQuery(params Guid[] Ids) : IQuery<IEnumerable<(Guid Id, string Username)>>;
