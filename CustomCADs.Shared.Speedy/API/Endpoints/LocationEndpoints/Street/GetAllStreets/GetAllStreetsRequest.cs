namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Street.GetAllStreets;

public record GetAllStreetsRequest(
    string UserName,
    string Password,
    string? Language,
    long? ClientSystemId
);