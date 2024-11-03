using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Users.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdDto>;
