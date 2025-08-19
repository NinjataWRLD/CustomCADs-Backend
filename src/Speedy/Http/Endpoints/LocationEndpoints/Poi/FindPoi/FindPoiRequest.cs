namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Poi.FindPoi;

internal record FindPoiRequest(
	string UserName,
	string Password,
	int SiteId,
	string? Language,
	long? ClientSystemId,
	string? Name
);
