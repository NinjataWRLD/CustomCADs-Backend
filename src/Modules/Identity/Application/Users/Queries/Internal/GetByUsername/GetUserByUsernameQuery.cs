using CustomCADs.Shared.Application.Abstractions.Requests.Queries;

namespace CustomCADs.Identity.Application.Users.Queries.Internal.GetByUsername;

public record GetUserByUsernameQuery(
	string Username
) : IQuery<GetUserByUsernameDto>;
