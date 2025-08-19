namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.State.GetAllStates;

internal record GetAllStatesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
