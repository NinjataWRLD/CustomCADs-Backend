namespace CustomCADs.Orders.Endpoints.Orders.Designer.Get.Reported;

public sealed record GetReportedOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);