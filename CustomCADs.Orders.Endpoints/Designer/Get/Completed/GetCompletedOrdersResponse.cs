namespace CustomCADs.Orders.Endpoints.Designer.Get.Completed;

public sealed record GetCompletedOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);