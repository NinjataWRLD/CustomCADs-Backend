namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.SecondaryShipment;

public record SecondaryShipmentRequest(
	string UserName,
	string Password,
	ShipmentType[] Types,
	string? Language,
	long? ClientSystemId
);
