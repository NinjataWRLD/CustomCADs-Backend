namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.GetContactByExternalId;

using Dtos.Client;

internal record GetContactByExternalIdResponse(
	ClientDto? Client,
	ErrorDto? Error
);
