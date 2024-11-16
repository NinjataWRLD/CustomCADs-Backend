namespace CustomCADs.Shared.Speedy.Services.ClientService.GetClient;

using Dtos.Client;
using Dtos.Errors;

public record GetClientResponse(
    ClientDto? Client,
    ErrorDto? Error
);