namespace CustomCADs.Speedy.Http.Endpoints.ValidationEndpoints.ValidatePhone;

internal record ValidatePhoneRequest(
	string UserName,
	string Password,
	string Number,
	string? Language,
	long? ClientSystemId,
	string? Ext
);
