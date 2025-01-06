namespace CustomCADs.Carts.Endpoints.ActiveCarts.Patch.IncrementQuantity;

public record IncreaseActiveCartItemQuantityRequest(
    Guid ItemId,
    int Amount = 1
);