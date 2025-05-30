namespace CustomCADs.Customs.Endpoints.Customs.Dtos;

public record FinishedCustomResponse(
	decimal Price,
	DateTimeOffset FinishedAt,
	Guid CadId
);
