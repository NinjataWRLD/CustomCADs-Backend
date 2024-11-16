namespace CustomCADs.Shared.Speedy.Services.LocationService.State.FindState;

using Dtos.State;

public record FindStateResponse(
    StateDto[]? States,
    ErrorDto? Error
);