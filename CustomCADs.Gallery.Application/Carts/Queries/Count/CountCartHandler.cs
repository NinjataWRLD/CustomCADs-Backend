using CustomCADs.Gallery.Domain.Carts.Reads;

namespace CustomCADs.Gallery.Application.Carts.Queries.Count;

public sealed class CountCartHandler(ICartReads reads)
    : IQueryHandler<CountCartQuery, int>
{
    public async Task<int> Handle(CountCartQuery req, CancellationToken ct)
    {
        return await reads.CountAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
    }
}
