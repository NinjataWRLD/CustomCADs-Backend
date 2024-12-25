using CustomCADs.Delivery.Application.Common.Exceptions;
using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Delivery.Domain.Shipments.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.Delivery.Application.Shipments.Queries.GetStatus;

public class GetShipmentTrackHandler(IShipmentReads reads, IDeliveryService delivery)
    : IQueryHandler<GetShipmentTrackQuery, Dictionary<DateTime, GetShipmentTrackDto>>
{
    public async Task<Dictionary<DateTime, GetShipmentTrackDto>> Handle(GetShipmentTrackQuery req, CancellationToken ct)
    {
        Shipment shipment = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ShipmentNotFoundException.ById(req.Id);

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
