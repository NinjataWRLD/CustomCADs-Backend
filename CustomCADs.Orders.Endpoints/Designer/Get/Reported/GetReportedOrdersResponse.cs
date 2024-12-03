namespace CustomCADs.Orders.Endpoints.Designer.Get.Reported;

public sealed record GetReportedOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);