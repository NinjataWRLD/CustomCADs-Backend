using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.Username;

public sealed class GetUsernamesByIdsHandler(IAccountReads reads)
    : IQueryHandler<GetUsernamesByIdsQuery, Dictionary<AccountId, string>>
{
    public async Task<Dictionary<AccountId, string>> Handle(GetUsernamesByIdsQuery req, CancellationToken ct)
    {
        AccountQuery query = new(
            Ids: req.Ids,
            Pagination: new(Limit: req.Ids.Length)
        );
        Result<Account> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return result.Items.ToDictionary(x => x.Id, x => x.Username);
    }
}
