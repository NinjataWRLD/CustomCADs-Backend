namespace CustomCADs.Shared.Speedy.Services.LocationService.Poi.GetPoi;

using Dtos.PointOfInterest;

public record GetPoiResponse(
    PointOfInterestDto? Poi,
    ErrorDto? Error
);