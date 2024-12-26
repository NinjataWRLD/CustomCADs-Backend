using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;

public sealed class DesignerGetOrderByIdHandler(IOrderReads reads)
    : IQueryHandler<DesignerGetOrderByIdQuery, DesignerGetOrderByIdDto>
{
    public async Task<DesignerGetOrderByIdDto> Handle(DesignerGetOrderByIdQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (order.OrderStatus is not OrderStatus.Pending && order.DesignerId != req.DesignerId)
        {
            throw OrderAuthorizationException.CannotViewNonPendingOrderNotAcceptedByYou();
        }

        return order.ToDesignerGetOrderByIdDto();
    }
}
