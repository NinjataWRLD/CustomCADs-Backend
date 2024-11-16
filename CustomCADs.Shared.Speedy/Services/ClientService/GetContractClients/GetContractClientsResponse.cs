namespace CustomCADs.Shared.Speedy.Services.ClientService.GetContractClients;

using Dtos.Client;
using Dtos.Errors;

public record GetContractClientsResponse(
    ClientDto[]? Clients,
    ErrorDto? Error
);