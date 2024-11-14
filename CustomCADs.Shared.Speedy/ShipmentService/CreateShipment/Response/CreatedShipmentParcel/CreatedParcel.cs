namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Response.CreatedShipmentParcel;

public record CreatedParcel(
    int SeqNo,
    string Id,
    int? ExternalCarrierId,
    string? ExternalCarrierParcelNumber
);
