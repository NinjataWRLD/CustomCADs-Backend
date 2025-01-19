namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.State.GetState;

using Dtos.State;

public record GetStateResponse(
    StateDto? State,
    ErrorDto? Error
);