namespace CustomCADs.Speedy.API.Endpoints.ClientEndpoints.CreateContact;

using Dtos.ShipmentSenderAndRecipient.ShipmentAddress;
using Dtos.ShipmentSenderAndRecipient.ShipmentPhoneNumber;

public record CreateContactRequest(
	string UserName,
	string Password,
	string ExternalContactId,
	ShipmentPhoneNumberDto Phone1,
	string ClientName,
	bool PrivatePerson,
	ShipmentAddressDto Address,
	string? Language,
	long? ClientSystemId,
	string? SecretKey,
	ShipmentPhoneNumberDto? Phone2,
	string? ObjectName,
	string? Email
);
