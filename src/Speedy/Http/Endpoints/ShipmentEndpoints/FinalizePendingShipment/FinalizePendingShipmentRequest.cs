namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.FinalizePendingShipment;

internal record FinalizePendingShipmentRequest(
	string UserName,
	string Password,
	string ShipmentId,
	string? Language,
	long? ClientSystemId
);
