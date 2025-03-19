using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.GetAll;

public sealed class GetAllPurchasedCartsHandler(IPurchasedCartReads reads, IRequestSender sender)
    : IQueryHandler<GetAllPurchasedCartsQuery, Result<GetAllPurchasedCartsDto>>
{
    public async Task<Result<GetAllPurchasedCartsDto>> Handle(GetAllPurchasedCartsQuery req, CancellationToken ct)
    {
        PurchasedCartQuery query = new(
            BuyerId: req.BuyerId,
            Sorting: req.Sorting,
            Pagination: req.Pagination
        );
        Result<PurchasedCart> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        AccountId[] buyerIds = [.. result.Items.Select(c => c.BuyerId)];
        GetTimeZonesByIdsQuery timeZonesQuery = new(buyerIds);
        Dictionary<AccountId, string> timeZones = await sender
            .SendQueryAsync(timeZonesQuery, ct).ConfigureAwait(false);

        return new(
            result.Count,
            [.. result.Items.Select(c => c.ToGetAllCartsItem(timeZones[c.BuyerId]))]
        );
    }
}
