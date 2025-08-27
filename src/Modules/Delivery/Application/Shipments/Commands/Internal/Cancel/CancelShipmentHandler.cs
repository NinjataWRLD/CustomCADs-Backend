using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Domain.Repositories.Reads;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Internal.Cancel;

public class CancelShipmentHandler(IShipmentReads reads, IDeliveryService delivery, BaseCachingService<ShipmentId, Shipment> cache)
	: ICommandHandler<CancelShipmentCommand>
{
	public async Task Handle(CancelShipmentCommand req, CancellationToken ct)
	{
		Shipment shipment = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Shipment>.ById(req.Id)
		).ConfigureAwait(false);

		await delivery.CancelAsync(
			shipmentId: shipment.ReferenceId,
			comment: req.Comment,
			ct: ct
		).ConfigureAwait(false);
	}
}
