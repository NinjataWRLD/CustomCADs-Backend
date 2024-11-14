namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentContent;

public record Parcel(
    Size Size,
    double Weight,
    string? Id,
    int? SeqNo
);
