using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Shared.Queries.Users.GetUsernameById;

public record GetUsernameByIdQuery(UserId Id) : IQuery<string>;
