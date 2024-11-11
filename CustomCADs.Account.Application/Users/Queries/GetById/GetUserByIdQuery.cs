using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Application.Users.Queries.GetById;

public record GetUserByIdQuery(UserId Id) : IQuery<GetUserByIdDto>;
