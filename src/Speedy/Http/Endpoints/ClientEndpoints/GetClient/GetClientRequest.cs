namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.GetClient;

internal record GetClientRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
