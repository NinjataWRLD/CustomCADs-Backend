namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.ShipmentInfo;

internal record ShipmentInfoRequest(
	string UserName,
	string Password,
	string[] ShipmentIds,
	string? Language,
	long? ClientSystemId
);
