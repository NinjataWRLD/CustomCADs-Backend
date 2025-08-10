namespace CustomCADs.Speedy.API.Endpoints.ValidationEndpoints;

public record ValidationResponse(
	bool? Valid,
	ErrorDto? Error
);
