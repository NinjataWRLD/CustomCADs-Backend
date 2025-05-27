using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts;

using static PurchasedCartsData;

public class PurchasedCartsBaseUnitTests
{
	protected static PurchasedCart CreateCart(AccountId? buyerId = null)
		=> PurchasedCart.Create(buyerId ?? ValidBuyerId1);

	protected static PurchasedCart CreateCartWithId(PurchasedCartId? id = null, AccountId? buyerId = null)
		=> PurchasedCart.CreateWithId(id ?? ValidId1, buyerId ?? ValidBuyerId1);

	protected static PurchasedCart CreateCartWithItems(
		AccountId buyerId,
		ActiveCartItem[] items,
		Dictionary<ProductId, decimal> prices,
		Dictionary<ProductId, CadId> productCads,
		Dictionary<CadId, CadId> itemCads
	)
	{
		var purchasedCart = CreateCart(buyerId);

		purchasedCart.AddItems([.. items.Select(item => {
			decimal price = prices[item.ProductId];
			CadId productCadId = productCads[item.ProductId];
			CadId itemCadId = itemCads[productCadId];

			return (price, itemCadId, item.ProductId, item.ForDelivery, item.CustomizationId, item.Quantity, item.AddedAt);
		})]);

		return purchasedCart;
	}
}
