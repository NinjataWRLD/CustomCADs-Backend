using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Queries.IsBuyer;

public class IsOrderBuyerHandler(IOrderReads reads)
    : IQueryHandler<IsOrderBuyerQuery, bool>
{
    public async Task<bool> Handle(IsOrderBuyerQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        return order.BuyerId == req.UserId;
    }
}
