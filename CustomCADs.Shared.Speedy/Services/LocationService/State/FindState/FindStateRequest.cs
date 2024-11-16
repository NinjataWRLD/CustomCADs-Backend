namespace CustomCADs.Shared.Speedy.Services.LocationService.State.FindState;

public record FindStateRequest(
    string UserName,
    string Password,
    int CountryId,
    string? Location,
    long? ClientSystemId,
    string? Name
);