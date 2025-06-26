using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Internal.Cancel;

public class CancelShipmentHandler(IShipmentReads reads, IDeliveryService delivery)
	: ICommandHandler<CancelShipmentCommand>
{
	public async Task Handle(CancelShipmentCommand req, CancellationToken ct)
	{
		Shipment shipment = await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Shipment>.ById(req.Id);

		await delivery.CancelAsync(
			shipmentId: shipment.ReferenceId,
			comment: req.Comment,
			ct
		).ConfigureAwait(false);
	}
}
