﻿using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items;

using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemsBaseUnitTests
{
	protected static PurchasedCartItem CreateItem(
		PurchasedCartId? cartId = null,
		ProductId? productId = null,
		CadId? cadId = null,
		CustomizationId? customizationId = null,
		decimal? price = null,
		int? quantity = null,
		bool? forDelivery = null,
		DateTimeOffset? addedAt = null
	) => PurchasedCartItem.Create(
			cartId: cartId ?? PurchasedCartsData.ValidId,
			productId: productId ?? ValidProductId,
			cadId: cadId ?? ValidCadId,
			price: price ?? MinValidPrice,
			quantity: quantity ?? MinValidQuantity,
			forDelivery: forDelivery ?? false,
			customizationId: customizationId,
			addedAt: addedAt ?? DateTimeOffset.UtcNow
		);
}
