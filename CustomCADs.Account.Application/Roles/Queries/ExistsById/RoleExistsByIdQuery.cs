using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Application.Roles.Queries.ExistsById;

public record RoleExistsByIdQuery(RoleId Id) : IQuery<bool>;
