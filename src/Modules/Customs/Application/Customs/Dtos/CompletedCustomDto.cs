using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Printing;

namespace CustomCADs.Customs.Application.Customs.Dtos;

public record CompletedCustomDto(
	PaymentStatus PaymentStatus,
	CustomizationId? CustomizationId,
	ShipmentId? ShipmentId
);
