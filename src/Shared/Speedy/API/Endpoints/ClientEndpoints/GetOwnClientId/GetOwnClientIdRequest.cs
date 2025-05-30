namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.GetOwnClientId;

public record GetOwnClientIdRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
