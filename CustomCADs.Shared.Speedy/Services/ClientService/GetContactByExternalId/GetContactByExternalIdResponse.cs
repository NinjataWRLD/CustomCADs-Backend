namespace CustomCADs.Shared.Speedy.Services.ClientService.GetContactByExternalId;

using Dtos.Client;
using Dtos.Errors;

public record GetContactByExternalIdResponse(
    ClientDto? Client,
    ErrorDto? Error
);