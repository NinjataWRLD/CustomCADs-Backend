using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Queries.GetUsersWithIds;

public class GetUsersWithIdsHandler(IUserReads reads)
{
    public async Task<Dictionary<Guid, GetUsersWithIdsDto>> Handle(GetUsersWithIdsQuery req, CancellationToken ct = default)
    {
        UsersQuery query = new()
        {
            Ids = req.Ids,
        };
        UserResult result = await reads.AllAsync(query, track: false, ct).ConfigureAwait(false);

        var response = result.Users.Select(u => new GetUsersWithIdsDto() 
        {
            Id = u.Id,
            Username = u.Username,
        }).ToDictionary(ks => ks.Id);
        return response;
    }
}
