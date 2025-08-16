namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Poi.GetAllPois;

internal record GetAllPoisRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
