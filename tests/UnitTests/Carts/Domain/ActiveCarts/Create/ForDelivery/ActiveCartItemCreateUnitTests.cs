using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.ForDelivery.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Create.ForDelivery;

public class ActiveCartItemCreateUnitTests : ActiveCartItemsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(ActiveCartItemCreateValidData))]
	public void Create_ShouldNotThrow_WhenCartIsValid(AccountId buyerId, ProductId productId, CustomizationId customizationId)
	{
		CreateItemWithDelivery(
			buyerId: buyerId,
			productId: productId,
			customizationId: customizationId
		);
	}

	[Theory]
	[ClassData(typeof(ActiveCartItemCreateValidData))]
	public void Create_ShouldPopulateProperties(AccountId buyerId, ProductId productId, CustomizationId customizationId)
	{
		var item = CreateItemWithDelivery(
			buyerId: buyerId,
			productId: productId,
			customizationId: customizationId
		);

		Assert.Multiple(
			() => Assert.Equal(buyerId, item.BuyerId),
			() => Assert.Equal(productId, item.ProductId),
			() => Assert.Equal(customizationId, item.CustomizationId),
			() => Assert.True(item.ForDelivery)
		);
	}
}
