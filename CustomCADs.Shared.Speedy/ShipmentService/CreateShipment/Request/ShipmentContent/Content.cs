namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentContent;

public record Content(
    string Contents,
    string Package,
    Parcel[] Parcels
);
