using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.Count;

public sealed class CountActiveCartItemsHandler(IActiveCartReads reads)
    : IQueryHandler<CountActiveCartItemsQuery, int>
{
    public async Task<int> Handle(CountActiveCartItemsQuery req, CancellationToken ct)
    {
        return await reads.CountAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
    }
}
