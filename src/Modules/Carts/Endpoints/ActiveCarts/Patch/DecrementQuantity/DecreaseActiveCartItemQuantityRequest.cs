namespace CustomCADs.Carts.Endpoints.ActiveCarts.Patch.DecrementQuantity;

public record DecreaseActiveCartItemQuantityRequest(
    Guid ProductId,
    int Amount = 1
);