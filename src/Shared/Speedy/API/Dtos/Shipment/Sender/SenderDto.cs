namespace CustomCADs.Shared.Speedy.API.Dtos.Shipment.Sender;

using ShipmentSenderAndRecipient.ShipmentAddress;

public record SenderDto(
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
