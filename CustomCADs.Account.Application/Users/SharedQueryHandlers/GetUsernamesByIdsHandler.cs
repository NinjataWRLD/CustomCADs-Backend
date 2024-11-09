using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Queries.Users.GetUsernamesByIds;

namespace CustomCADs.Account.Application.Users.SharedQueryHandlers;

public class GetUsernamesByIdsHandler(IUserReads reads)
    : IQueryHandler<GetUsernamesByIdsQuery, IEnumerable<(Guid Id, string Username)>>
{
    public async Task<IEnumerable<(Guid Id, string Username)>> Handle(GetUsernamesByIdsQuery req, CancellationToken ct)
    {
        UsersQuery query = new(Ids: req.Ids);
        UserResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        var response = result.Users.Select(u => (u.Id, u.Username));
        return response;
    }
}
