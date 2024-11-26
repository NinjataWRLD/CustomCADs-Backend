using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;

public class DesignerGetOrderByIdHandler(IOrderReads reads)
    : IQueryHandler<DesignerGetOrderByIdQuery, DesignerGetOrderByIdDto>
{
    public async Task<DesignerGetOrderByIdDto> Handle(DesignerGetOrderByIdQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (order.OrderStatus is not OrderStatus.Pending
            && order.DesignerId != req.DesignerId)
        {
            throw OrderValidationException.Custom("");
        }

        return order.ToDesignerGetOrderByIdDto();
    }
}
