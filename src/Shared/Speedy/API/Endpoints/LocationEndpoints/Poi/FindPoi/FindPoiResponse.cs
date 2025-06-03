namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Poi.FindPoi;

using Dtos.PointOfInterest;

public record FindPoiResponse(
	PointOfInterestDto[]? Pois,
	ErrorDto? Error
);
