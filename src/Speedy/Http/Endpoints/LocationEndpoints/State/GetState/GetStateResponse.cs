namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.State.GetState;

using Dtos.State;

internal record GetStateResponse(
	StateDto? State,
	ErrorDto? Error
);
