namespace CustomCADs.Speedy.Http.Dtos.Shipment.Sender;

using ShipmentSenderAndRecipient.ShipmentAddress;

internal record SenderDto(
	int DropoffOfficeId,
	string DropoffGeoPUDOId,

	// Copied from Client
	long ClientId,
	string ClientName,
	string ObjectName,
	string ContactName,
	AddressDto Address,
	string Email,
	bool PrivatePerson
);
