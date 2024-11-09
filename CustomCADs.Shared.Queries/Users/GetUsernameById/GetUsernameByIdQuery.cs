using CustomCADs.Shared.Application.Requests.Queries;

namespace CustomCADs.Shared.Queries.Users.GetUsernameById;

public record GetUsernameByIdQuery(Guid Id) : IQuery<string>;
