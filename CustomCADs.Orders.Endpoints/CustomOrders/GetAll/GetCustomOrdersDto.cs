using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Orders.Endpoints.CustomOrders.GetAll;

public record GetCustomOrdersDto(
    Guid Id,
    string Name,
    string OrderDate,
    string DeliveryType,
    string OrderStatus,
    ImageDto Image
);