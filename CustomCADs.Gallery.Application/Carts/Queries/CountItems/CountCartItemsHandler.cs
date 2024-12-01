using CustomCADs.Gallery.Domain.Carts.Reads;

namespace CustomCADs.Gallery.Application.Carts.Queries.CountItems;

public class CountCartItemsHandler(ICartReads reads)
    : IQueryHandler<CountCartItemsQuery, Dictionary<CartId, int>>
{
    public async Task<Dictionary<CartId, int>> Handle(CountCartItemsQuery req, CancellationToken ct)
    {
        return await reads.CountItemsAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
    }
}
