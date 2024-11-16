namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.State.GetState;

public record GetStateRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);