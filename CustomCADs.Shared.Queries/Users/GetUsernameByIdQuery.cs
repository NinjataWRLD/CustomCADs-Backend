using CustomCADs.Shared.Application.Requests.Queries;

namespace CustomCADs.Shared.Queries.Users;

public record GetUsernameByIdQuery(UserId Id) : IQuery<string>;
