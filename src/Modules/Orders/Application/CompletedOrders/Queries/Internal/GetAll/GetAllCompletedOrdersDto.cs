namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.GetAll;

public record GetAllCompletedOrdersDto(
    CompletedOrderId Id,
    string Name,
    bool Delivery,
    DateTimeOffset OrderedAt,
    DateTimeOffset PurchasedAt,
    string BuyerName,
    string? DesignerName
);