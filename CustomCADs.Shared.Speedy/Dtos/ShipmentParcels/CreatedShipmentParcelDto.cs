namespace CustomCADs.Shared.Speedy.Dtos.ShipmentParcels;

public record CreatedShipmentParcelDto(
    int SeqNo,
    string Id,
    int? ExternalCarrierId,
    string? ExternalCarrierParcelNumber
);
