using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Behaviors.AddItems;

using static PurchasedCartConstants;
using static PurchasedCartsData.CartItemsData;

public class PurchasedCartAddItemsUnitTests : PurchasedCartsBaseUnitTests
{
	[Fact]
	public void AddItems_ShouldNotThrowException_WhenItemsCountIsValid()
	{
		CreateCartWithId().AddItems([
			(MaxValidPrice, ValidCadId, ValidProductId, false, null, 1, DateTimeOffset.UtcNow)
		]);
	}

	[Fact]
	public void AddItems_ShouldThrowException_WhenItemsCountIsNotValid()
	{
		var purchasedCart = CreateCartWithId();
		for (int i = 0; i < ItemsCountMax; i++)
		{
			purchasedCart.AddItems([
				(MaxValidPrice, ValidCadId, ProductId.New(), false, null, 1, DateTimeOffset.UtcNow)
			]);
		}

		Assert.Throws<CustomValidationException<PurchasedCart>>(
			() => purchasedCart.AddItems([
				(MaxValidPrice, ValidCadId, ProductId.New(), false, null, 1, DateTimeOffset.UtcNow)
			])
		);
	}
}
