using CustomCADs.Delivery.Application.Common.Exceptions;
using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Delivery.Domain.Shipments.Reads;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Edit;

public class EditShipmentHandler(IShipmentReads reads, IUnitOfWork uow)
    : ICommandHandler<EditShipmentCommand>
{
    public async Task Handle(EditShipmentCommand req, CancellationToken ct)
    {
        Shipment shipment = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ShipmentNotFoundException.ById(req.Id);

        if (shipment.BuyerId != req.BuyerId)
        {
            throw ShipmentAuthorizationException.ById(req.Id);
        }

        shipment.SetAddress(req.Address);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
