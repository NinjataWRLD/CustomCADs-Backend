namespace CustomCADs.Account.Application.Users.Queries.GetById;

public record GetUserByIdQuery(UserId Id) : IQuery<GetUserByIdDto>;
