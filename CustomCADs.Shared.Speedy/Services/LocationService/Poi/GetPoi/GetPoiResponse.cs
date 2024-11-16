namespace CustomCADs.Shared.Speedy.Services.LocationService.Poi.GetPoi;

using Dtos.Errors;
using Dtos.PointOfInterest;

public record GetPoiResponse(
    PointOfInterestDto? Poi,
    ErrorDto? Error
);