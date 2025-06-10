using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Customs.Application.Customs.Dtos;

public record CompletedCustomDto(
	PaymentStatus PaymentStatus,
	CustomizationId? CustomizationId,
	ShipmentId? ShipmentId
);
