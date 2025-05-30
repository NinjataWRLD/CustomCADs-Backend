namespace CustomCADs.Customs.Endpoints.Customs.Dtos;

public record AcceptedCustomResponse(
	DateTimeOffset AcceptedAt,
	string DesignerName
);
