namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Poi.FindPoi;

using Dtos.PointOfInterest;

internal record FindPoiResponse(
	PointOfInterestDto[]? Pois,
	ErrorDto? Error
);
