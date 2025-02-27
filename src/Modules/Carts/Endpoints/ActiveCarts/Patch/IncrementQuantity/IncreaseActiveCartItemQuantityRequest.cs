namespace CustomCADs.Carts.Endpoints.ActiveCarts.Patch.IncrementQuantity;

public record IncreaseActiveCartItemQuantityRequest(
    Guid ProductId,
    int Amount = 1
);