namespace CustomCADs.Speedy.Http.Dtos.Shipment.Recipient;

using ShipmentSenderAndRecipient.ShipmentAddress;

/// <summary>
/// DTO representing recipient details for a shipment.
/// </summary>
/// <param name="PickupOfficeId">Pickup office identifier.</param>
/// <param name="PickupGeoPUDOId">Pickup GeoPUDO identifier.</param>
/// <param name="ClientId">Client identifier.</param>
/// <param name="ClientName">Name of the client.</param>
/// <param name="ObjectName">Name of the object.</param>
/// <param name="ContactName">Contact person's name.</param>
/// <param name="Address">Recipient address details.</param>
/// <param name="Email">Recipient email address.</param>
/// <param name="PrivatePerson">Indicates if the recipient is a private person.</param>
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
