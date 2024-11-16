namespace CustomCADs.Shared.Speedy.Services.LocationService.State.GetAllStates;

public record GetAllStatesRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);