namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Street.GetAllStreets;

internal record GetAllStreetsRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
