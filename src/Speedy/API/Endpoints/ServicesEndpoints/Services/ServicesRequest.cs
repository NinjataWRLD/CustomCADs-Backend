namespace CustomCADs.Speedy.API.Endpoints.ServicesEndpoints.Services;

public record ServicesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId,
	string? Date
);
