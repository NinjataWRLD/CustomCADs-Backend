namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.GetOwnClientId;

internal record GetOwnClientIdRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
