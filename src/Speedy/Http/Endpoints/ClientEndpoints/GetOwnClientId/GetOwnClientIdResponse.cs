namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.GetOwnClientId;

internal record GetOwnClientIdResponse(
	long ClientId,
	ErrorDto? Error
);
