namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Street.FindStreet;

internal record FindStreetRequest(
	string UserName,
	string Password,
	int SiteId,
	string? Language,
	long? ClientSystemId,
	string? Name,
	string? Type
);
