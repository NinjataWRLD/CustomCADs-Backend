namespace CustomCADs.Shared.Speedy.API.Dtos.Errors;

public record ErrorDto(
	string Id,
	int Code,
	string Message,
	string? Context,
	string? Component
);
