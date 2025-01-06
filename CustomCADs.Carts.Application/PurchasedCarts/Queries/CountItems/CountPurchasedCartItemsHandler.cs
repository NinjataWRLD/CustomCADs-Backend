using CustomCADs.Carts.Domain.PurchasedCarts.Reads;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.CountItems;

public sealed class CountPurchasedCartItemsHandler(IPurchasedCartReads reads)
    : IQueryHandler<CountPurchasedCartItemsQuery, Dictionary<PurchasedCartId, int>>
{
    public async Task<Dictionary<PurchasedCartId, int>> Handle(CountPurchasedCartItemsQuery req, CancellationToken ct)
    {
        return await reads.CountItemsAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
    }
}
