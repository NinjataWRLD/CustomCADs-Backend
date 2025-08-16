namespace CustomCADs.Speedy.Http.Dtos.Client;

using ShipmentSenderAndRecipient.ShipmentAddress;

internal record ClientDto(
	long ClientId,
	string ClientName,
	string ObjectName,
	string ContactName,
	AddressDto Address,
	string Email,
	bool PrivatePerson
);
