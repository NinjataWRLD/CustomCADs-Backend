namespace CustomCADs.Shared.Speedy.Models.Shipment.Parcel;

public record ShipmentParcelRefModel(
    string? Id,
    string? ExternalCarrierParcelNumber,
    string? FullBarcode
);
