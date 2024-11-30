using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Accounts.Application.Accounts.SharedQueryHandlers;

public class GetTimeZoneByIdsHandler(IAccountReads reads)
    : IQueryHandler<GetTimeZonesByIdsQuery, (AccountId Id, string TimeZone)[]>
{
    public async Task<(AccountId Id, string TimeZone)[]> Handle(GetTimeZonesByIdsQuery req, CancellationToken ct)
    {
        AccountQuery query = new(Ids: req.Ids);
        Result<Account> result = await reads.AllAsync(query, track: false, ct: ct);

        return [.. result.Items.Select(a => (a.Id, a.TimeZone))];
    }
}
