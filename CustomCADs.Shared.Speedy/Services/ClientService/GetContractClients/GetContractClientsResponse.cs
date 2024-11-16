namespace CustomCADs.Shared.Speedy.Services.ClientService.GetContractClients;

using Dtos.Client;

public record GetContractClientsResponse(
    ClientDto[]? Clients,
    ErrorDto? Error
);