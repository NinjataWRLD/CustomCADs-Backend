namespace CustomCADs.Shared.Speedy.Services.LocationService.Poi.FindPoi;

public record FindPoiRequest(
    string UserName,
    string Password,
    int SiteId,
    string? Location,
    long? ClientSystemId,
    string? Name
);