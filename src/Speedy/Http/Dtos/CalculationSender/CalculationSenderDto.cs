namespace CustomCADs.Speedy.Http.Dtos.CalculationSender;

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
/// <param name="DropoffOfficeId">
/// 	Drop-off office id
/// 	<br />
/// 	Requried: if address location and clientId are missing. If address location presents it is forbidden.
/// </param>
/// <param name="DropoffGeoPUDOId">
/// 	DPD drop off office PUDO id
/// 	<br />
/// 	Requried: No. Must be empty if dropoffOfficeId is provided. Should refer to a valid accessible DPD office.
/// </param>
internal record CalculationSenderDto(
	long? ClientId,
	bool? PrivatePerson,
	CalculationAddressLocationDto? AddressLocation,
	int? DropoffOfficeId,
	string? DropoffGeoPUDOId
);
