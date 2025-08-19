namespace CustomCADs.Speedy.Core.Contracts.Shipment;

public record CreatedShipmentParcelModel(
	int SeqNo,
	string Id,
	int? ExternalCarrierId,
	string? ExternalCarrierParcelNumber
);
