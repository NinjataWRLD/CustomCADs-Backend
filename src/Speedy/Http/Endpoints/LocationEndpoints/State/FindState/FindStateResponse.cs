namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.State.FindState;

using Dtos.State;

internal record FindStateResponse(
	StateDto[]? States,
	ErrorDto? Error
);
