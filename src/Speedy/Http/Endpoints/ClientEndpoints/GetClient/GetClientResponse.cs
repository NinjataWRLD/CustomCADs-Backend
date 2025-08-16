namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.GetClient;

using Dtos.Client;

internal record GetClientResponse(
	ClientDto? Client,
	ErrorDto? Error
);
