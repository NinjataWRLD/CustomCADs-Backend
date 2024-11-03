using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Users.Queries.ExistsByUsername;

public record UserExistsByUsernameQuery(string Username) : IQuery<bool>;