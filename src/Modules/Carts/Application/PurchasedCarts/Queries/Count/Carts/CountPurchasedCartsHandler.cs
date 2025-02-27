using CustomCADs.Carts.Domain.PurchasedCarts.Reads;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Count.Carts;

public sealed class CountPurchasedCartsHandler(IPurchasedCartReads reads)
    : IQueryHandler<CountPurchasedCartsQuery, int>
{
    public async Task<int> Handle(CountPurchasedCartsQuery req, CancellationToken ct)
    {
        return await reads.CountAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
    }
}
