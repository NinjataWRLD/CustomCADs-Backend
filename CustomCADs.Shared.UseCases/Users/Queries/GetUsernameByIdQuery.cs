namespace CustomCADs.Shared.UseCases.Users.Queries;

public record GetUsernameByIdQuery(UserId Id) : IQuery<string>;
