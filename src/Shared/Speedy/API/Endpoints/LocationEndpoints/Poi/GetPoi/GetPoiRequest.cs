namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Poi.GetPoi;

public record GetPoiRequest(
    string UserName,
    string Password,
    string? Language,
    long? ClientSystemId
);