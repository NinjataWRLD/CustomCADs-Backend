namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Complex.GetComplex;

public record GetComplexRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
