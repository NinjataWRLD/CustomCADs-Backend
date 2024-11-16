namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.GetContractClients;

public record GetContractClientsRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);