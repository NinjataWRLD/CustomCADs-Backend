namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Street.FindStreet;

public record FindStreetRequest(
	string UserName,
	string Password,
	int SiteId,
	string? Language,
	long? ClientSystemId,
	string? Name,
	string? Type
);
