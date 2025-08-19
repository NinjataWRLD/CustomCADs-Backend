namespace CustomCADs.Speedy.Http.Endpoints.ServicesEndpoints.Services;

internal record ServicesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId,
	string? Date
);
