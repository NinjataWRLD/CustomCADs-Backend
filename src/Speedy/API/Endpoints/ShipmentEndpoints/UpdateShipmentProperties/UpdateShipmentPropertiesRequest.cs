namespace CustomCADs.Speedy.API.Endpoints.ShipmentEndpoints.UpdateShipmentProperties;

public record UpdateShipmentPropertiesRequest(
	string UserName,
	string Password,
	string Id,
	Dictionary<string, string> Properties,
	string? Language,
	long? ClientSystemId
);
