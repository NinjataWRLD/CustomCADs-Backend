using CustomCADs.Carts.Domain.PurchasedCarts.Reads;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Count;

public sealed class CountPurchasedCartHandler(IPurchasedCartReads reads)
    : IQueryHandler<CountPurchasedCartsQuery, int>
{
    public async Task<int> Handle(CountPurchasedCartsQuery req, CancellationToken ct)
    {
        return await reads.CountAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
    }
}
