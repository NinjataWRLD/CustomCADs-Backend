namespace CustomCADs.Orders.Endpoints.Designer.Get.Accepted;

public sealed record GetAcceptedOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);