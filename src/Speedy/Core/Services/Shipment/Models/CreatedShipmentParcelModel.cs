namespace CustomCADs.Speedy.Core.Services.Shipment.Models;

public record CreatedShipmentParcelModel(
	int SeqNo,
	string Id,
	int? ExternalCarrierId,
	string? ExternalCarrierParcelNumber
);
