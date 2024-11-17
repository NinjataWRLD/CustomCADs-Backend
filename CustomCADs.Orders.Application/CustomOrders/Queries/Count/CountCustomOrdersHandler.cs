using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Queries.Count;

public class CountCustomOrdersHandler(ICustomOrderReads reads)
    : IQueryHandler<CountCustomOrdersQuery, int>
{
    public async Task<int> Handle(CountCustomOrdersQuery req, CancellationToken ct)
    {
        return await reads.CountByStatusAsync(req.BuyerId, req.Status, ct: ct).ConfigureAwait(false);
    }
}
