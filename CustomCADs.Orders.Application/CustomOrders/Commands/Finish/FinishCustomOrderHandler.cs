using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Finish;

public class FinishCustomOrderHandler(ICustomOrderReads reads, IUnitOfWork uow)
    : ICommandHandler<FinishCustomOrderCommand>
{
    public async Task Handle(FinishCustomOrderCommand req, CancellationToken ct)
    {
        CustomOrder order = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomOrderNotFoundException.ById(req.Id);

        if (req.FinisherId != order.DesignerId)
        {
            throw CustomOrderValidationException.Custom("Cannot finish an order you aren't associated with.");
        }
        order.SetFinishedStatus();

        if (order.DeliveryType is DeliveryType.Digital or DeliveryType.Both)
        {
            if (req.CadId is null)
            {
                throw CustomOrderValidationException.Custom("Cannot finish a Digital delivery type order without providing a CadId.");
            }
            order.SetCadId(req.CadId.Value);
        }

        if (order.DeliveryType is DeliveryType.Physical or DeliveryType.Both)
        {
            if (req.ShipmentId is null)
            {
                throw CustomOrderValidationException.Custom("Cannot finish with a Physical delivery type order without providing a ShipmentId.");
            }
            order.SetShipmentId(req.ShipmentId.Value);
        }

        await uow.SaveChangesAsync(ct);
    }
}
