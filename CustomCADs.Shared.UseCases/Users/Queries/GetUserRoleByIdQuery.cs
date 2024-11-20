namespace CustomCADs.Shared.UseCases.Users.Queries;

public record GetUserRoleByIdQuery(UserId Id) : IQuery<string>;
