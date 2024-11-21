namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Poi.FindPoi;

public record FindPoiRequest(
    string UserName,
    string Password,
    int SiteId,
    string? Language,
    long? ClientSystemId,
    string? Name
);