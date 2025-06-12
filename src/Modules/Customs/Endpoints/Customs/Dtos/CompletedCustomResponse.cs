using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Endpoints.Customs.Dtos;

public record CompletedCustomResponse(
	PaymentStatus PaymentStatus,
	Guid? CustomizationId,
	Guid? ShipmentId
);
