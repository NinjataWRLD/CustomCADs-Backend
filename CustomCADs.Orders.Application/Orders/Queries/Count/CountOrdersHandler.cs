using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Queries.Count;

public class CountOrdersHandler(IOrderReads reads)
    : IQueryHandler<CountOrdersQuery, int>
{
    public async Task<int> Handle(CountOrdersQuery req, CancellationToken ct)
    {
        return await reads.CountByStatusAsync(req.BuyerId, req.Status, ct: ct).ConfigureAwait(false);
    }
}
