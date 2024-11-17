using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Cancel;

public class CancelCustomOrderHandler(ICustomOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<CancelCustomOrderCommand>
{
    public async Task Handle(CancelCustomOrderCommand req, CancellationToken ct)
    {
        CustomOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomOrderNotFoundException.ById(req.Id);

        if (req.CancellerId != order.DesignerId)
        {
            throw CustomOrderValidationException.Custom("Cannot cancel an Order you aren't associated with.");
        }

        order.SetPendingStatus();
        order.EraseDesignerId();

        await uow.SaveChangesAsync(ct);
    }
}

