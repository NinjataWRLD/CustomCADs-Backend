namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.UpdateShipmentProperties;

internal record UpdateShipmentPropertiesRequest(
	string UserName,
	string Password,
	string Id,
	Dictionary<string, string> Properties,
	string? Language,
	long? ClientSystemId
);
