namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.SecondaryShipment;

internal record SecondaryShipmentRequest(
	string UserName,
	string Password,
	ShipmentType[] Types,
	string? Language,
	long? ClientSystemId
);
