using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders.Entities;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Commands.Cancel;

public class CancelOrderHandler(IOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<CancelOrderCommand>
{
    public async Task Handle(CancelOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (req.CancellerId != order.DesignerId)
        {
            throw OrderValidationException.Custom("Cannot cancel an Order you aren't associated with.");
        }

        order.SetPendingStatus();
        order.EraseDesignerId();

        await uow.SaveChangesAsync(ct);
    }
}

