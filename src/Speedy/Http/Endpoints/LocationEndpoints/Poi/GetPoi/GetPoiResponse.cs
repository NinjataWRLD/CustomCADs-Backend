namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Poi.GetPoi;

using Dtos.PointOfInterest;

internal record GetPoiResponse(
	PointOfInterestDto? Poi,
	ErrorDto? Error
);
