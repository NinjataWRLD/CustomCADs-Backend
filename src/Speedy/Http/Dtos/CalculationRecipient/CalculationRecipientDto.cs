namespace CustomCADs.Speedy.Http.Dtos.CalculationRecipient;

using CalculationAddressLocation;

/// <param name="ClientId">
/// 	Client id to refer serving client
/// 	<br />
/// 	Required: No.
/// </param>
/// <param name="PrivatePerson">
/// 	Private person flag
/// 	<br />
/// 	Requried: if clientId is provided, it is forbidden. Otherwise, it is mandatory.
/// </param>
/// <param name="AddressLocation">
/// 	Address location, implies pickup at client address
/// 	<br />
/// 	Requried: if office id and clientId are missing. Otherwise it is forbidden
/// </param>
/// <param name="PickupOfficeId">
/// 	Pickup office id
/// 	<br />
/// 	Requried: if address location and clientId are missing. If address location presents it is forbidden
/// </param>
/// <param name="PickupGeoPUDOId">
/// 	DPD pickup office PUDO id
/// 	<br />
/// 	Requried: No. Must be empty if pickupOfficeId is provided. Should refer to a valid accessible DPD office.
/// </param>
internal record CalculationRecipientDto(
	CalculationAddressLocationDto? AddressLocation,
	long? ClientId,
	bool? PrivatePerson,
	int? PickupOfficeId,
	string? PickupGeoPUDOId
);
