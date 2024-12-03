namespace CustomCADs.Orders.Endpoints.Orders.Designer.Get.Pending;

public sealed record GetPendingOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);