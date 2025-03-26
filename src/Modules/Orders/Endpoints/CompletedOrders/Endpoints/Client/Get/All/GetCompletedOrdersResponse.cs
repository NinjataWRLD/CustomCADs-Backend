namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.All;

public sealed record GetCompletedOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string PurchaseDate,
    bool Delivery
);