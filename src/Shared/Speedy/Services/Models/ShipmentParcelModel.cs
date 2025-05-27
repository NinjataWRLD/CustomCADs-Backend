namespace CustomCADs.Shared.Speedy.Services.Models;

public record ShipmentParcelModel(
	double Weight,
	string? Id,
	int? SeqNo,
	long? PackageUniqueNumber,
	string? Ref1,
	string? Ref2,
	ShipmentParcelSizeModel? Size,
	ExternalCarrierParcelNumberModel? PickupExternalCarrierParcelNumber,
	ExternalCarrierParcelNumberModel? DeliveryExternalCarrierParcelNumber
);
