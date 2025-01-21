namespace CustomCADs.Carts.Endpoints.ActiveCarts.Get.Single;

public sealed record GetActiveCartResponse(
    Guid Id,
    string BuyerName,
    ICollection<ActiveCartItemResponse> Items
);
