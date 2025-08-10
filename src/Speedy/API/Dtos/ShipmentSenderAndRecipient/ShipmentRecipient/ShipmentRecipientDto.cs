namespace CustomCADs.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentRecipient;

using AutoSelectNearestOfficePolicy;
using ShipmentAddress;
using ShipmentPhoneNumber;

public record ShipmentRecipientDto(
	bool? PrivatePerson,
	string? ContactName,
	string? Email,
	long? ClientId,
	string? ClientName,
	string? ObjectName,
	int? PickupOfficeId,
	string? PickupGeoPUDOIf,
	bool? AutoSelectNearestOffice,
	AutoSelectNearestOfficePolicyDto? AutoSelectNearestOfficePolicy,
	ShipmentAddressDto? Address,
	ShipmentPhoneNumberDto? Phone1,
	ShipmentPhoneNumberDto? Phone2,
	ShipmentPhoneNumberDto? Phone3
);
