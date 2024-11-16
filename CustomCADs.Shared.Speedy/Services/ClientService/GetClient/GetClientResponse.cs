namespace CustomCADs.Shared.Speedy.Services.ClientService.GetClient;

using Dtos.Client;

public record GetClientResponse(
    ClientDto? Client,
    ErrorDto? Error
);