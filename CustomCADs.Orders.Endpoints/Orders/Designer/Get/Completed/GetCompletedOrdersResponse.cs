namespace CustomCADs.Orders.Endpoints.Orders.Designer.Get.Completed;

public sealed record GetCompletedOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);