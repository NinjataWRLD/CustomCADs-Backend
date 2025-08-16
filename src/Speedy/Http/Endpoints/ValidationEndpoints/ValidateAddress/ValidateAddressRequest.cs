namespace CustomCADs.Speedy.Http.Endpoints.ValidationEndpoints.ValidateAddress;

using Dtos.ShipmentSenderAndRecipient.ShipmentAddress;

internal record ValidateAddressRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId,
	ShipmentAddressDto Address
);
