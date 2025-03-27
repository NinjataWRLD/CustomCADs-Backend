namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;

public record GetShipmentTrackQuery(
    ShipmentId Id
) : IQuery<Dictionary<DateTime, GetShipmentTrackDto>>;
