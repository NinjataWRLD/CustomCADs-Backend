namespace CustomCADs.Speedy.API.Dtos.ShipmentParcels;

public record ShipmentParcelRefDto(
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);
