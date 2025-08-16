namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Complex.GetComplex;

internal record GetComplexRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
