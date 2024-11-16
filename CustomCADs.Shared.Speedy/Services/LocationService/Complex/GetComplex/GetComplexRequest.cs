namespace CustomCADs.Shared.Speedy.Services.LocationService.Complex.GetComplex;

public record GetComplexRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);