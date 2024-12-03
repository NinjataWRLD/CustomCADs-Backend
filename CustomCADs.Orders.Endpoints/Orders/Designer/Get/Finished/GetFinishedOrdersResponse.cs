namespace CustomCADs.Orders.Endpoints.Orders.Designer.Get.Finished;

public sealed record GetFinishedOrdersResponse(
    Guid Id,
    string Name,
    string DeliveryType,
    string OrderDate,
    string BuyerName
);