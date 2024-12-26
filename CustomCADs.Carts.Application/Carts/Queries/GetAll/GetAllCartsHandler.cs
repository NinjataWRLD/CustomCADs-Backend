using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.Carts.Queries.GetAll;

public sealed class GetAllCartsHandler(ICartReads reads, IRequestSender sender)
    : IQueryHandler<GetAllCartsQuery, Result<GetAllCartsDto>>
{
    public async Task<Result<GetAllCartsDto>> Handle(GetAllCartsQuery req, CancellationToken ct)
    {
        CartQuery query = new(
            BuyerId: req.BuyerId,
            Sorting: req.Sorting,
            Pagination: req.Pagination
        );
        Result<Cart> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        AccountId[] buyerIds = [.. result.Items.Select(c => c.BuyerId)];
        GetTimeZonesByIdsQuery timeZonesQuery = new(buyerIds);
        (AccountId Id, string TimeZone)[] timeZones = await sender
            .SendQueryAsync(timeZonesQuery, ct).ConfigureAwait(false);

        return new(
            result.Count,
            result.Items.Select(c =>
                c.ToGetAllCartsItem(
                    timeZone: timeZones.Single(t => t.Id == c.BuyerId).TimeZone
            )).ToArray()
        );
    }
}
