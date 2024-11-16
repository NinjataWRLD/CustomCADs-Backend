namespace CustomCADs.Shared.Speedy.Services.ClientService.GetContractClients;

public record GetContractClientsRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);