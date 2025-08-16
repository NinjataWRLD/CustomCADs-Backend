namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.GetContractClients;

using Dtos.Client;

internal record GetContractClientsResponse(
	ClientDto[]? Clients,
	ErrorDto? Error
);
