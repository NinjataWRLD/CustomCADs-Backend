namespace CustomCADs.Speedy.Http.Endpoints.ValidationEndpoints;

internal record ValidationResponse(
	bool? Valid,
	ErrorDto? Error
);
