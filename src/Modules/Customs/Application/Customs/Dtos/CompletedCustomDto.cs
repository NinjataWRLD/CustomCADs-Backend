using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Customs.Application.Customs.Dtos;

public record CompletedCustomDto(
    CustomizationId? CustomizationId,
    ShipmentId? ShipmentId
);
