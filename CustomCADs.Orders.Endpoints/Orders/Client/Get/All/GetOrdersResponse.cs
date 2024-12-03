namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.All;

public sealed record GetOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string DeliveryType,
    string OrderStatus
);