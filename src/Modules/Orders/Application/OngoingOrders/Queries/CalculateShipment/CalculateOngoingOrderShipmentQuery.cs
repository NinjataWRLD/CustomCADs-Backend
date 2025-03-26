using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.CalculateShipment;

public record CalculateOngoingOrderShipmentQuery(
    OngoingOrderId Id,
    int Count,
    AddressDto Address,
    CustomizationId CustomizationId
) : IQuery<CalculateShipmentDto[]>;