using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Client.CalculateShipment;

public record CalculateCustomShipmentQuery(
    CustomId Id,
    int Count,
    AddressDto Address,
    CustomizationId CustomizationId
) : IQuery<CalculateShipmentDto[]>;