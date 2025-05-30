namespace CustomCADs.Customs.Application.Customs.Dtos;

public record AcceptedCustomDto(
	DateTimeOffset AcceptedAt,
	string DesignerName
);
