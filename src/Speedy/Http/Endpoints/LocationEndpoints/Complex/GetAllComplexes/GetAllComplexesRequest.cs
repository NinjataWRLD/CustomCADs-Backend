namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Complex.GetAllComplexes;

internal record GetAllComplexesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
