namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.State.FindState;

internal record FindStateRequest(
	string UserName,
	string Password,
	int CountryId,
	string? Language,
	long? ClientSystemId,
	string? Name
);
