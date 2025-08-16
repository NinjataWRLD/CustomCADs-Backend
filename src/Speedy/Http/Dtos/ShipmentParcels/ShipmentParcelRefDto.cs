namespace CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

internal record ShipmentParcelRefDto(
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);
