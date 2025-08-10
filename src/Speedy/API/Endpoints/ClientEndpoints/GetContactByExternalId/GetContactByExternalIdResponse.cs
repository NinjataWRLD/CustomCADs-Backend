namespace CustomCADs.Speedy.API.Endpoints.ClientEndpoints.GetContactByExternalId;

using Dtos.Client;

public record GetContactByExternalIdResponse(
	ClientDto? Client,
	ErrorDto? Error
);
