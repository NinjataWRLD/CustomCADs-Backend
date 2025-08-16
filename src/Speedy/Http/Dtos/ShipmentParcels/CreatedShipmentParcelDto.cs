namespace CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

internal record CreatedShipmentParcelDto(
	int SeqNo,
	string Id,
	int? ExternalCarrierId,
	string? ExternalCarrierParcelNumber
);
