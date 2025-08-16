namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Recipient;

public record ShipmentRecipientModel(
	bool? PrivatePerson,
	string? ContactName,
	string? Email,
	long? ClientId,
	string? ClientName,
	string? ObjectName,
	int? PickupOfficeId,
	string? PickupGeoPUDOId,
	bool? AutoSelectNearestOffice,
	AutoSelectNearestOfficePolicyModel? AutoSelectNearestOfficePolicy,
	ShipmentAddressModel? Address,
	PhoneNumberModel? Phone1,
	PhoneNumberModel? Phone2,
	PhoneNumberModel? Phone3
);
