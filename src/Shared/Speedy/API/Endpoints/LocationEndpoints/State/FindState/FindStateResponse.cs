namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.State.FindState;

using Dtos.State;

public record FindStateResponse(
	StateDto[]? States,
	ErrorDto? Error
);
