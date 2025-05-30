namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Poi.GetPoi;

using Dtos.PointOfInterest;

public record GetPoiResponse(
	PointOfInterestDto? Poi,
	ErrorDto? Error
);
