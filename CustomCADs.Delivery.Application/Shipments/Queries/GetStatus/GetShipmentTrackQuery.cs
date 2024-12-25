namespace CustomCADs.Delivery.Application.Shipments.Queries.GetStatus;

public record GetShipmentTrackQuery(
    ShipmentId Id
) : IQuery<Dictionary<DateTime, GetShipmentTrackDto>>;
