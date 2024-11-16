namespace CustomCADs.Shared.Speedy.Services.LocationService.Poi.GetPoi;

public record GetPoiRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);