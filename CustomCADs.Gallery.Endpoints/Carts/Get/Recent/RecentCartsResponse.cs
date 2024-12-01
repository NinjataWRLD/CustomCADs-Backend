namespace CustomCADs.Gallery.Endpoints.Carts.Get.Recent;

public record RecentCartsResponse(
    Guid Id,
    string PurchaseDate
);