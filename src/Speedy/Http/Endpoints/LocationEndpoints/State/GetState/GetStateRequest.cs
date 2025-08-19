namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.State.GetState;

internal record GetStateRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
