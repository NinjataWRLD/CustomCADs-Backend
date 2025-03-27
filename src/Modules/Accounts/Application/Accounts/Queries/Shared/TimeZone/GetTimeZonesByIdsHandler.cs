using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Shared.TimeZone;

public sealed class GetTimeZonesByIdsHandler(IAccountReads reads)
    : IQueryHandler<GetTimeZonesByIdsQuery, Dictionary<AccountId, string>>
{
    public async Task<Dictionary<AccountId, string>> Handle(GetTimeZonesByIdsQuery req, CancellationToken ct)
    {
        AccountQuery query = new(
            Ids: req.Ids,
            Pagination: new(Limit: req.Ids.Length)
        );
        Result<Account> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return result.Items.ToDictionary(a => a.Id, x => x.TimeZone);
    }
}
