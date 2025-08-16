namespace CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

internal record ParcelHandoverDto(
	string DateTime,

	// Copied from ShipmentParcelRefDto
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);
