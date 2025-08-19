namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Street.GetStreet;

internal record GetStreetRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
