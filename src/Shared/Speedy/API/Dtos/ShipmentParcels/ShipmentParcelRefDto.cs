namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentParcels;

public record ShipmentParcelRefDto(
    string? Id,
    string? ExternalCarrierParcelNumber,
    string? FullBarcode
);
