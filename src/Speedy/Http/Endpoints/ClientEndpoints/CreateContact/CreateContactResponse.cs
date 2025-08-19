namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.CreateContact;

internal record CreateContactResponse(
	long ClientId,
	ErrorDto? Error
);
