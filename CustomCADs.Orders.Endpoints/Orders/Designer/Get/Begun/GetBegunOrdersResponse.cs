namespace CustomCADs.Orders.Endpoints.Orders.Designer.Get.Begun;

public sealed record GetBegunOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);