namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.GetAll;

public record GetAllCompletedOrdersDto(
    CompletedOrderId Id,
    string Name,
    bool Delivery,
    DateTime OrderDate,
    DateTime PurchaseDate,
    string BuyerName,
    string? DesignerName
);