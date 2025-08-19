namespace CustomCADs.Speedy.Core.Models.Shipment.Sender;

public record ShipmentSenderModel(
	PhoneNumberModel Phone1,
	PhoneNumberModel? Phone2,
	PhoneNumberModel? Phone3,
	ShipmentAddressModel? Address,
	string? Email,
	long? ClientId,
	string? ClientName,
	string? ContactName,
	bool? PrivatePerson,
	int? DropoffOfficeId,
	string? DropoffGeoPUDOId
);
