using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Orders.Application.Orders.Queries.CalculateShipment;

public record CalculateOrderShipmentQuery(
    OrderId Id,
    double TotalWeight,
    AddressDto Address
) : IQuery<CalculateOrderShipmentDto[]>;