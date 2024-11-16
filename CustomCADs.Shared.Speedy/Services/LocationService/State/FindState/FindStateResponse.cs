namespace CustomCADs.Shared.Speedy.Services.LocationService.State.FindState;

using Dtos.Errors;
using Dtos.State;

public record FindStateResponse(
    StateDto[]? States,
    ErrorDto? Error
);