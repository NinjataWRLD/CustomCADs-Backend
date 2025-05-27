namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Patch.ToggleForDelivery;

public record ToggleActiveCartItemForDeliveryRequest(
	Guid ProductId,
	Guid? CustomizationId
);
