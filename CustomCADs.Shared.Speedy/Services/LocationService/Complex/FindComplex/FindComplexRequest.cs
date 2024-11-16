namespace CustomCADs.Shared.Speedy.Services.LocationService.Complex.FindComplex;

public record FindComplexRequest(
    string UserName,
    string Password,
    int SiteId,
    string? Location,
    long? ClientSystemId,
    string? Name,
    string? Type
);