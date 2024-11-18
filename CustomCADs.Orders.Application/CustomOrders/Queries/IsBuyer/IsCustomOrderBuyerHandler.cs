using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Queries.IsBuyer;

public class IsCustomOrderBuyerHandler(ICustomOrderReads reads)
    : IQueryHandler<IsCustomOrderBuyerQuery, bool>
{
    public async Task<bool> Handle(IsCustomOrderBuyerQuery req, CancellationToken ct)
    {
        CustomOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomOrderNotFoundException.ById(req.Id);

        return order.BuyerId == req.UserId;
    }
}
