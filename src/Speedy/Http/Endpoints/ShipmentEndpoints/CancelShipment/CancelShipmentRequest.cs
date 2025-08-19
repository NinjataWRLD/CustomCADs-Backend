namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.CancelShipment;

internal record CancelShipmentRequest(
	string UserName,
	string Password,
	string ShipmentId,
	string Comment,
	string? Language,
	long? ClientSystemId
);
