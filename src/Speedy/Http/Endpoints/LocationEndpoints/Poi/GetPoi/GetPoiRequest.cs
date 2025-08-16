namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Poi.GetPoi;

internal record GetPoiRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
