using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Account.Application.Roles.Queries.ExistsById;

public record RoleExistsByIdQuery(RoleId Id) : IQuery<bool>;
