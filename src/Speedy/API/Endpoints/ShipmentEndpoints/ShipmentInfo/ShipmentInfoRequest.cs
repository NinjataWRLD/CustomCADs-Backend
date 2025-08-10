namespace CustomCADs.Speedy.API.Endpoints.ShipmentEndpoints.ShipmentInfo;

public record ShipmentInfoRequest(
	string UserName,
	string Password,
	string[] ShipmentIds,
	string? Language,
	long? ClientSystemId
);
