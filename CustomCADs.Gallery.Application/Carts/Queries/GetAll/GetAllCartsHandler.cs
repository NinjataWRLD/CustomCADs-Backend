using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetAll;

public class GetAllCartsHandler(ICartReads reads, IRequestSender sender)
    : IQueryHandler<GetAllCartsQuery, Result<GetAllCartsDto>>
{
    public async Task<Result<GetAllCartsDto>> Handle(GetAllCartsQuery req, CancellationToken ct)
    {
        CartQuery query = new(
            BuyerId: req.BuyerId,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        Result<Cart> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        AccountId[] buyerIds = [.. result.Items.Select(c => c.BuyerId)];
        GetTimeZonesByIdsQuery timeZonesQuery = new(buyerIds);
        (AccountId Id, string TimeZone)[] timeZones = await sender
            .SendQueryAsync(timeZonesQuery, ct)
            .ConfigureAwait(false);

        return new(
            result.Count,
            result.Items.Select(c =>
                c.ToGetAllCartsItem(
                    timeZone: timeZones.Single(t => t.Id == c.BuyerId).TimeZone
            )).ToArray()
        );
    }
}
