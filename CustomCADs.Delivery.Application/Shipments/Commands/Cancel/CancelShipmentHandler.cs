using CustomCADs.Delivery.Application.Common.Exceptions;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Cancel;

public class CancelShipmentHandler(IShipmentReads reads, IDeliveryService delivery)
    : ICommandHandler<CancelShipmentCommand>
{
    public async Task Handle(CancelShipmentCommand req, CancellationToken ct)
    {
        Shipment shipment = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
            ?? throw ShipmentNotFoundException.ById(req.Id);

        await delivery.CancelAsync(
            shipmentId: shipment.ReferenceId,
            comment: req.Comment,
            ct
        ).ConfigureAwait(false);
    }
}
