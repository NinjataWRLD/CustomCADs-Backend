namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.State.FindState;

public record FindStateRequest(
    string UserName,
    string Password,
    int CountryId,
    string? Language,
    long? ClientSystemId,
    string? Name
);