using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts;

using static PurchasedCartsData;
using static PurchasedCartsData.CartItemsData;

public class PurchasedCartsBaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static PurchasedCart CreateCartWithId(PurchasedCartId? id = null, AccountId? buyerId = null)
		=> PurchasedCart.CreateWithId(
			id: id ?? ValidId,
			buyerId: buyerId ?? ValidBuyerId
		);

	protected static PurchasedCart CreateCartWithItems(PurchasedCartId? id = null, AccountId? buyerId = null, params PurchasedCartItem[] items)
	{
		var purchasedCart = CreateCartWithId(id, buyerId);
		purchasedCart.AddItems([.. items.Select(i =>
			(
				Price: i.Price,
				CadId: i.CadId,
				ProductId: i.ProductId,
				ForDelivery: i.ForDelivery,
				CustomizationId: i.CustomizationId,
				Quantity: i.Quantity,
				AddedAt: i.AddedAt
			)
		)]);

		return purchasedCart;
	}

	protected static PurchasedCartItem CreateItem(
		PurchasedCartId? cartId = null,
		ProductId? productId = null,
		CadId? cadId = null,
		CustomizationId? customizationId = null,
		decimal? price = null,
		int? quantity = null,
		bool? forDelivery = null
	) => PurchasedCartItem.Create(
			cartId: cartId ?? ValidId,
			productId: productId ?? ValidProductId,
			cadId: cadId ?? ValidCadId,
			customizationId: customizationId ?? ValidCustomizationId,
			price: price ?? MinValidPrice,
			quantity: quantity ?? MinValidQuantity,
			forDelivery: forDelivery ?? false,
				addedAt: DateTimeOffset.UtcNow
		);
}
