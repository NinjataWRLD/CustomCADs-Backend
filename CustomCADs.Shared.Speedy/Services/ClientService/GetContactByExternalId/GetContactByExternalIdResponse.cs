namespace CustomCADs.Shared.Speedy.Services.ClientService.GetContactByExternalId;

using Dtos.Client;

public record GetContactByExternalIdResponse(
    ClientDto? Client,
    ErrorDto? Error
);