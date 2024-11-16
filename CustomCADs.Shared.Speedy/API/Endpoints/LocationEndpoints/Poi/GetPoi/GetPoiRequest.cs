namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Poi.GetPoi;

public record GetPoiRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);