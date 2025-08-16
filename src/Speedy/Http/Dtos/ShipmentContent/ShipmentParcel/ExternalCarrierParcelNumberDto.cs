namespace CustomCADs.Speedy.Http.Dtos.ShipmentContent.ShipmentParcel;

internal record ExternalCarrierParcelNumberDto(
	Carrier ExternalCarrier,
	string ParcelNumber
);
