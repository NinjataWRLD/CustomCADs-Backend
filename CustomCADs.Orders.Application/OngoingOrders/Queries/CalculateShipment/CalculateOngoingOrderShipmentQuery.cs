using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.CalculateShipment;

public record CalculateOngoingOrderShipmentQuery(
    OngoingOrderId Id,
    double TotalWeight,
    AddressDto Address
) : IQuery<CalculateOngoingOrderShipmentDto[]>;