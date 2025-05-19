namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.CreateContact;

public record CreateContactResponse(
	long ClientId,
	ErrorDto? Error
);
