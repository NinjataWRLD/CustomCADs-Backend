using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
using CustomCADs.Shared.Queries.Users.GetUsernamesByIds;

namespace CustomCADs.Account.Application.Users.SharedQueryHandlers;

public class GetUsernamesByIdsHandler(IUserReads reads)
    : IQueryHandler<GetUsernamesByIdsQuery, IEnumerable<(UserId Id, string Username)>>
{
    public async Task<IEnumerable<(UserId Id, string Username)>> Handle(GetUsernamesByIdsQuery req, CancellationToken ct)
    {
        UserQuery query = new(Ids: req.Ids);
        UserResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        var response = result.Users.Select(u => (u.Id, u.Username));
        return response;
    }
}
