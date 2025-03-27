namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.Single;

public sealed record GetActiveCartResponse(
    Guid Id,
    string BuyerName,
    ICollection<ActiveCartItemResponse> Items
);
