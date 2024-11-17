using CustomCADs.Shared.Application.Requests.Queries;

namespace CustomCADs.Shared.Queries.Users;

public record GetUserRoleByIdQuery(UserId Id) : IQuery<string>;
