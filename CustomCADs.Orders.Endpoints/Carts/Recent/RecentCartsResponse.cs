namespace CustomCADs.Orders.Endpoints.Carts.Recent;

public record RecentCartsResponse(
    Guid Id,
    string PurchaseDate
);