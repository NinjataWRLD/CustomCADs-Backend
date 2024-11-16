namespace CustomCADs.Shared.Speedy.Services.LocationService.Complex.GetAllComplexes;

public record GetAllComplexesRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);