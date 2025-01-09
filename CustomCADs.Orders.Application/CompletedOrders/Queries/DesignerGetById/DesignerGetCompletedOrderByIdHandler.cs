using CustomCADs.Orders.Domain.CompletedOrders.Reads;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;

public sealed class DesignerGetCompletedOrderByIdHandler(ICompletedOrderReads reads)
    : IQueryHandler<DesignerGetCompletedOrderByIdQuery, DesignerGetCompletedOrderByIdDto>
{
    public async Task<DesignerGetCompletedOrderByIdDto> Handle(DesignerGetCompletedOrderByIdQuery req, CancellationToken ct)
    {
        CompletedOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CompletedOrderNotFoundException.ById(req.Id);

        if (order.DesignerId != req.DesignerId)
        {
            throw CompletedOrderAuthorizationException.NotAssociated(order.Id, "view");
        }

        return order.ToDesignerGetOrderByIdDto();
    }
}
