namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Street.GetAllStreets;

public record GetAllComplexRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);