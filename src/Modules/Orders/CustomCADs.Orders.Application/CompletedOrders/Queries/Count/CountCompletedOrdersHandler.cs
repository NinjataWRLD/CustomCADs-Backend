using CustomCADs.Orders.Domain.CompletedOrders.Reads;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Count;

public sealed class CountCompletedOrdersHandler(ICompletedOrderReads reads)
    : IQueryHandler<CountCompletedOrdersQuery, int>
{
    public async Task<int> Handle(CountCompletedOrdersQuery req, CancellationToken ct)
    {
        return await reads
            .CountByBuyerIdAsync(req.BuyerId, ct: ct)
            .ConfigureAwait(false);
    }
}
