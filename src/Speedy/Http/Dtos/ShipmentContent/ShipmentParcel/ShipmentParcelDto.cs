namespace CustomCADs.Speedy.Http.Dtos.ShipmentContent.ShipmentParcel;

internal record ShipmentParcelDto(
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
