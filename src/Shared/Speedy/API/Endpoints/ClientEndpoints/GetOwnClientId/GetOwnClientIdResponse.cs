namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.GetOwnClientId;

public record GetOwnClientIdResponse(
	long ClientId,
	ErrorDto? Error
);
