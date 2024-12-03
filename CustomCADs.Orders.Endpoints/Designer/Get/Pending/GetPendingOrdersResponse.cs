namespace CustomCADs.Orders.Endpoints.Designer.Get.Pending;

public sealed record GetPendingOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);