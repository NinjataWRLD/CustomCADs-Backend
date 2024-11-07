using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.GetUsersWithIds;

public class GetUsersWithIdsHandler(IUserReads reads)
    : IQueryHandler<GetUsersWithIdsQuery, IEnumerable<GetUsersWithIdsDto>>
{
    public async Task<IEnumerable<GetUsersWithIdsDto>> Handle(GetUsersWithIdsQuery req, CancellationToken ct = default)
    {
        UsersQuery query = new()
        {
            Ids = req.Ids,
        };
        UserResult result = await reads.AllAsync(query, track: false, ct).ConfigureAwait(false);

        var response = result.Users.Select(u => new GetUsersWithIdsDto(u.Id, u.Username));
        return response;
    }
}
