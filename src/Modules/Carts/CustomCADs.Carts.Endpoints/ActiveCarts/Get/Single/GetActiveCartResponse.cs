namespace CustomCADs.Carts.Endpoints.ActiveCarts.Get.Single;

public sealed record GetActiveCartResponse(
    Guid Id,
    Guid BuyerId,
    ICollection<ActiveCartItemResponse> Items
);
