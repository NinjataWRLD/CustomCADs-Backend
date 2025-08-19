namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.GetContactByExternalId;

internal record GetContactByExternalIdRequest(
	string UserName,
	string Password,
	string? Langauge,
	long? ClientSystemId,
	string? SecretKey
);
