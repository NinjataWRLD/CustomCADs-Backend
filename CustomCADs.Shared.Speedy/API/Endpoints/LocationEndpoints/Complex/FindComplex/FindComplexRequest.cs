namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Complex.FindComplex;

public record FindComplexRequest(
    string UserName,
    string Password,
    int SiteId,
    string? Location,
    long? ClientSystemId,
    string? Name,
    string? Type
);