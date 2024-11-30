namespace CustomCADs.Shared.UseCases.Users.Queries;

public record GetTimeZonesByIdsQuery(params UserId[] Ids) 
    : IQuery<(UserId Id, string TimeZone)[]>;
