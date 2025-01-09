using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.DesignerGetById;

public sealed class DesignerGetOngoingOrderByIdHandler(IOngoingOrderReads reads)
    : IQueryHandler<DesignerGetOngoingOrderByIdQuery, DesignerGetOngoingOrderByIdDto>
{
    public async Task<DesignerGetOngoingOrderByIdDto> Handle(DesignerGetOngoingOrderByIdQuery req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        if (order.OrderStatus is not OngoingOrderStatus.Pending
            && order.DesignerId != req.DesignerId)
        {
            throw OngoingOrderAuthorizationException.NotAssociated(order.Id, "view");
        }

        return order.ToDesignerGetOrderByIdDto();
    }
}
