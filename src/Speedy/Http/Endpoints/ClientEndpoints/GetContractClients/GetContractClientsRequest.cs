namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.GetContractClients;

internal record GetContractClientsRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
