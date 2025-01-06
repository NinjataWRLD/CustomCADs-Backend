namespace CustomCADs.Carts.Endpoints.ActiveCarts.Patch.DecrementQuantity;

public record DecreaseActiveCartItemQuantityRequest(
    Guid ItemId,
    int Amount = 1
);