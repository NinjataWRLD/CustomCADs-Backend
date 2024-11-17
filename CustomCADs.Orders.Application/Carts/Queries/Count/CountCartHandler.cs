using CustomCADs.Orders.Domain.Carts.Reads;

namespace CustomCADs.Orders.Application.Carts.Queries.Count;

public class CountCartHandler(ICartReads reads)
    : IQueryHandler<CountCartQuery, int>
{
    public async Task<int> Handle(CountCartQuery req, CancellationToken ct)
    {
        return await reads.CountAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
    }
}
