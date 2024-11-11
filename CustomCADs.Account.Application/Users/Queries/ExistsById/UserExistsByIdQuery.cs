using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Application.Users.Queries.ExistsById;

public record UserExistsByIdQuery(UserId Id) : IQuery<bool>;
