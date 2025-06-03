namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Complex.GetAllComplexes;

public record GetAllComplexesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
