namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentContent.ShipmentParcel;

public record ShipmentParcelDto(
	double Weight,
	string? Id,
	int? SeqNo,
	long? PackageUniqueNumber,
	string? Ref1,
	string? Ref2,
	ShipmentParcelSizeDto? Size,
	ExternalCarrierParcelNumberDto? PickupExternalCarrierParcelNumber,
	ExternalCarrierParcelNumberDto? DeliveryExternalCarrierParcelNumber
);
