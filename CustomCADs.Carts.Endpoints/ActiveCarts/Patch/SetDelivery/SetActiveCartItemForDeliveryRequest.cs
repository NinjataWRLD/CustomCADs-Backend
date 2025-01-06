namespace CustomCADs.Carts.Endpoints.ActiveCarts.Patch.SetDelivery;

public record SetActiveCartItemForDeliveryRequest(
    Guid ItemId,
    bool Value
);
