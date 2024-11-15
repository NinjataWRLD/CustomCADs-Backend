namespace CustomCADs.Shared.Speedy.Dtos.ShipmentParcels;

public record ShipmentParcelRefDto(
    string? Id,
    string? ExternalCarrierParcelNumber,
    string? FullBarcode
);
