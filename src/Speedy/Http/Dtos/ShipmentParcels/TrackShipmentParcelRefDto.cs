namespace CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

internal record TrackShipmentParcelRefDto(
	string? Ref,

	// Copied from ShipmentParcelRefDto
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);
