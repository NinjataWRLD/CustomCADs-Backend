namespace CustomCADs.Speedy.API.Endpoints.ClientEndpoints.GetClient;

using Dtos.Client;

public record GetClientResponse(
	ClientDto? Client,
	ErrorDto? Error
);
