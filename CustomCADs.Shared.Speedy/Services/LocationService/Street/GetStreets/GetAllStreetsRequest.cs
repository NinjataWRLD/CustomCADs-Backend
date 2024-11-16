namespace CustomCADs.Shared.Speedy.Services.LocationService.Street.GetAllStreets;

public record GetAllComplexRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);