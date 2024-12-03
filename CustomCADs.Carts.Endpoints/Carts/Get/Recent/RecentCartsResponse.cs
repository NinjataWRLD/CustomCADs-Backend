namespace CustomCADs.Carts.Endpoints.Carts.Get.Recent;

public sealed record RecentCartsResponse(
    Guid Id,
    string PurchaseDate
);