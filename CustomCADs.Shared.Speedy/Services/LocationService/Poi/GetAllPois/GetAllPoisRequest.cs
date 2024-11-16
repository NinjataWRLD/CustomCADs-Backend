namespace CustomCADs.Shared.Speedy.Services.LocationService.Poi.GetAllPois;

public record GetAllPoisRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);