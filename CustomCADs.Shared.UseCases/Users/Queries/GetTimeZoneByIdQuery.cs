namespace CustomCADs.Shared.UseCases.Users.Queries;

public record GetTimeZoneByIdQuery(UserId Id) : IQuery<string>;
