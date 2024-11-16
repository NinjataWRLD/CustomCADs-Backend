namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.GetContractClients;

using Dtos.Client;

public record GetContractClientsResponse(
    ClientDto[]? Clients,
    ErrorDto? Error
);