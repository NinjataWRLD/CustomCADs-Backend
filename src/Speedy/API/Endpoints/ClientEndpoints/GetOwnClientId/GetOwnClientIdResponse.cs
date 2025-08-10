namespace CustomCADs.Speedy.API.Endpoints.ClientEndpoints.GetOwnClientId;

public record GetOwnClientIdResponse(
	long ClientId,
	ErrorDto? Error
);
