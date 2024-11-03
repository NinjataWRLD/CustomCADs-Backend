using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Users.Queries.GetByUsername;

public record GetUserByUsernameQuery(string Username) : IQuery<GetUserByUsernameDto>;