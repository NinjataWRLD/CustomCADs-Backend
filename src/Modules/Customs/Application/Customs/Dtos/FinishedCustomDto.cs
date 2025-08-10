using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Customs.Application.Customs.Dtos;

public record FinishedCustomDto(
	decimal Price,
	DateTimeOffset FinishedAt,
	CadId CadId
);
