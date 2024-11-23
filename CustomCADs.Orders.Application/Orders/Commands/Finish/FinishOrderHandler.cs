using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public class FinishOrderHandler(IOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<FinishOrderCommand>
{
    public async Task Handle(FinishOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (req.FinisherId != order.DesignerId)
        {
            throw OrderValidationException.Custom("Cannot finish an order you aren't associated with.");
        }
        order.SetFinishedStatus();

        if (order.DeliveryType is DeliveryType.Digital or DeliveryType.Both)
        {
            if (req.CadId is null)
            {
                throw OrderValidationException.Custom("Cannot finish a Digital delivery type order without providing a CadId.");
            }
            order.SetCadId(req.CadId.Value);
        }

        await uow.SaveChangesAsync(ct);
    }
}
