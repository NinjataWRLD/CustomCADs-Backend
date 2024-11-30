using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Account.Application.Users.SharedQueryHandlers;

public class GetTimeZoneByIdsHandler(IUserReads reads)
    : IQueryHandler<GetTimeZonesByIdsQuery, (UserId Id, string TimeZone)[]>
{
    public async Task<(UserId Id, string TimeZone)[]> Handle(GetTimeZonesByIdsQuery req, CancellationToken ct)
    {
        UserQuery query = new(Ids: req.Ids);
        Result<User> result = await reads.AllAsync(query, track: false, ct: ct);

        return [.. result.Items.Select(u => (Id: u.Id, TimeZone: u.TimeZone))];
    }
}
