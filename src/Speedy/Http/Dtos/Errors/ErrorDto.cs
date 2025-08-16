namespace CustomCADs.Speedy.Http.Dtos.Errors;

internal record ErrorDto(
	string Id,
	int Code,
	string Message,
	string? Context,
	string? Component
);
