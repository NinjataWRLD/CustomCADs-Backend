namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.GetContractClients;

public record GetContractClientsRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
