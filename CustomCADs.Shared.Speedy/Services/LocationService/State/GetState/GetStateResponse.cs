namespace CustomCADs.Shared.Speedy.Services.LocationService.State.GetState;

using Dtos.State;

public record GetStateResponse(
    StateDto? State,
    ErrorDto? Error
);