namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentParcels;

public record CreatedShipmentParcelDto(
	int SeqNo,
	string Id,
	int? ExternalCarrierId,
	string? ExternalCarrierParcelNumber
);
