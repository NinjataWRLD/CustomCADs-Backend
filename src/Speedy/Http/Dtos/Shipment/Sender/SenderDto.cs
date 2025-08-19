namespace CustomCADs.Speedy.Http.Dtos.Shipment.Sender;

using ShipmentSenderAndRecipient.ShipmentAddress;

/// <summary>
/// DTO representing sender details for a shipment.
/// </summary>
/// <param name="DropoffOfficeId">Dropoff office identifier.</param>
/// <param name="DropoffGeoPUDOId">Dropoff GeoPUDO identifier.</param>
/// <param name="ClientId">Client identifier.</param>
/// <param name="ClientName">Name of the client.</param>
/// <param name="ObjectName">Name of the object.</param>
/// <param name="ContactName">Contact person's name.</param>
/// <param name="Address">Sender address details.</param>
/// <param name="Email">Sender email address.</param>
/// <param name="PrivatePerson">Indicates if the sender is a private person.</param>
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
