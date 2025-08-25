using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;

public class GetShipmentTrackHandler(IShipmentReads reads, IDeliveryService delivery, BaseCachingService<ShipmentId, Shipment> cache)
	: IQueryHandler<GetShipmentTrackQuery, Dictionary<DateTimeOffset, GetShipmentTrackDto>>
{
	public async Task<Dictionary<DateTimeOffset, GetShipmentTrackDto>> Handle(GetShipmentTrackQuery req, CancellationToken ct)
	{
		Shipment shipment = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Shipment>.ById(req.Id)
		).ConfigureAwait(false);

		ShipmentStatusDto[] statuses = await delivery.TrackAsync(
			shipment.ReferenceId,
			ct
		).ConfigureAwait(false);

		var response = statuses.ToDictionary(
			x => x.DateTime,
			x => new GetShipmentTrackDto(x.Message, x.Place)
		);
		return response;
	}
}
