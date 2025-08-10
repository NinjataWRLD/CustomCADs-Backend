using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Customs.Application.Customs.Dtos;

public record CompletedCustomDto(
	PaymentStatus PaymentStatus,
	CustomizationId? CustomizationId,
	ShipmentId? ShipmentId
);
