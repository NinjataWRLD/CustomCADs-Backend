using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Account.Application.Users.SharedQueryHandlers;

public class GetUsernamesByIdsHandler(IUserReads reads)
    : IQueryHandler<GetUsernamesByIdsQuery, IEnumerable<(UserId Id, string Username)>>
{
    public async Task<IEnumerable<(UserId Id, string Username)>> Handle(GetUsernamesByIdsQuery req, CancellationToken ct)
    {
        UserQuery query = new(Ids: req.Ids);
        UserResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return result.Users.Select(u => (u.Id, u.Username));
    }
}
