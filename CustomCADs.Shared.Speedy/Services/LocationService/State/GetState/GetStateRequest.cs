namespace CustomCADs.Shared.Speedy.Services.LocationService.State.GetState;

public record GetStateRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);