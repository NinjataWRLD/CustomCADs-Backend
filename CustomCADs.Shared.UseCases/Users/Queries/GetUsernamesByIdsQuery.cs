namespace CustomCADs.Shared.UseCases.Users.Queries;

public record GetUsernamesByIdsQuery(params UserId[] Ids)
    : IQuery<IEnumerable<(UserId Id, string Username)>>;
