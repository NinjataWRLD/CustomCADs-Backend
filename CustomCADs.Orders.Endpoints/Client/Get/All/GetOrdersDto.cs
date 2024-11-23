using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Orders.Endpoints.Client.Get.All;

public record GetOrdersDto(
    Guid Id,
    string Name,
    string OrderDate,
    string DeliveryType,
    string OrderStatus,
    ImageDto Image
);