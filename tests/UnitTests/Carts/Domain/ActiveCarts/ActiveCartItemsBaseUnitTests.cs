using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts;

using static ActiveCartsData;

public class ActiveCartItemsBaseUnitTests
{
	protected static ActiveCartItem CreateItem(
		AccountId? buyerId = null,
		ProductId? productId = null
	) => ActiveCartItem.Create(
			buyerId: buyerId ?? ValidBuyerId1,
			productId: productId ?? ValidProductId1
		);

	protected static ActiveCartItem CreateItemWithDelivery(
		AccountId? buyerId = null,
		ProductId? productId = null,
		CustomizationId? customizationId = null
	) => ActiveCartItem.Create(
			buyerId: buyerId ?? ValidBuyerId1,
			productId: productId ?? ValidProductId1,
			customizationId: customizationId ?? ValidCustomizationId1
		);
}
