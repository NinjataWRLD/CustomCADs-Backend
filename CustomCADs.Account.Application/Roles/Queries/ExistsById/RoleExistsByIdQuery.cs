using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Roles.Queries.ExistsById;

public record RoleExistsByIdQuery(int Id) : IQuery<bool>;
