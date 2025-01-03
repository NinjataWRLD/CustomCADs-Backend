using CustomCADs.Carts.Domain.Carts.Reads;

namespace CustomCADs.Carts.Application.Carts.Queries.CountItems;

public sealed class CountCartItemsHandler(ICartReads reads)
    : IQueryHandler<CountCartItemsQuery, Dictionary<CartId, int>>
{
    public async Task<Dictionary<CartId, int>> Handle(CountCartItemsQuery req, CancellationToken ct)
    {
        return await reads.CountItemsByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
    }
}
