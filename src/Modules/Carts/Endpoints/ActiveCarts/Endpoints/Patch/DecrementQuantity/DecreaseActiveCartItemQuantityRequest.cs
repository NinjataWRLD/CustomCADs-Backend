﻿namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Patch.DecrementQuantity;

public record DecreaseActiveCartItemQuantityRequest(
	Guid ProductId,
	int Amount = 1
);
