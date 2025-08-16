namespace CustomCADs.Speedy.Http.Dtos.Shipment.Recipient;

using ShipmentSenderAndRecipient.ShipmentAddress;

internal record RecipientDto(
	int PickupOfficeId,
	string PickupGeoPUDOId,

	// Copied from Client
	long ClientId,
	string ClientName,
	string ObjectName,
	string ContactName,
	AddressDto Address,
	string Email,
	bool PrivatePerson
);
