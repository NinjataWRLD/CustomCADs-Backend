namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.FinalizePendingShipment;

public record FinalizePendingShipmentRequest(
	string UserName,
	string Password,
	string ShipmentId,
	string? Language,
	long? ClientSystemId
);
