namespace CustomCADs.Orders.Endpoints.Orders.Designer.Get.Accepted;

public sealed record GetAcceptedOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);