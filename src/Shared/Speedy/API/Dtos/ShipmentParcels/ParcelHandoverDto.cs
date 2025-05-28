namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentParcels;

public record ParcelHandoverDto(
	string DateTime,

	// Copied from ShipmentParcelRefDto
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);
