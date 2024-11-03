using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Users.Queries.ExistsById;

public record UserExistsByIdQuery(Guid Id) : IQuery<bool>;
