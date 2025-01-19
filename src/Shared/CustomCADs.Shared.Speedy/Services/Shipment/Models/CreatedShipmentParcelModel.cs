namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record CreatedShipmentParcelModel(
    int SeqNo,
    string Id,
    int? ExternalCarrierId,
    string? ExternalCarrierParcelNumber
);
