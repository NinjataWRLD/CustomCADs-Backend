namespace CustomCADs.Speedy.Http.Dtos.ShipmentContent.ShipmentParcel;

/// <param name="ExternalCarrier">
/// 	External Carrier reference name
/// 	<br />
/// 	Required: Yes
/// </param>
/// <param name="ParcelNumber">
/// 	External Carrier parcel number
/// 	<br />
/// 	Required: Yes
/// </param>
internal record ExternalCarrierParcelNumberDto(
	Carrier ExternalCarrier,
	string ParcelNumber
);
