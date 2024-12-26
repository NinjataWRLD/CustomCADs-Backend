using CustomCADs.Delivery.Application.Common.Exceptions;
using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Delivery.Domain.Shipments.Reads;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Delete;

public class DeleteShipmentHandler(IShipmentReads reads, IWrites<Shipment> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteShipmentCommand>
{
    public async Task Handle(DeleteShipmentCommand req, CancellationToken ct)
    {
        Shipment shipment = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ShipmentNotFoundException.ById(req.Id);

        if (shipment.BuyerId != req.BuyerId)
        {
            throw ShipmentAuthorizationException.ById(req.Id);
        }

        writes.Remove(shipment);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
