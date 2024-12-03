namespace CustomCADs.Orders.Endpoints.Client.Get.All;

public sealed record GetOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string DeliveryType,
    string OrderStatus
);