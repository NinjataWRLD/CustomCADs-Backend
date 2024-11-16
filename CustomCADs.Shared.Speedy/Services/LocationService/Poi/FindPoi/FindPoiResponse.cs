namespace CustomCADs.Shared.Speedy.Services.LocationService.Poi.FindPoi;

using Dtos.Errors;
using Dtos.PointOfInterest;

public record FindPoiResponse(
    PointOfInterestDto[]? Pois,
    ErrorDto? Error
);