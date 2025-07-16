namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Create.Normal;

using CustomCADs.UnitTests.Carts.Domain.PurchasedCarts;
using static PurchasedCartsData;

public class PurchasedCartCreateUnitTests : PurchasedCartsBaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowException()
	{
		CreateCart(buyerId: ValidBuyerId);
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		var cart = CreateCart(buyerId: ValidBuyerId);

		Assert.Multiple(
			() => Assert.Equal(ValidBuyerId, cart.BuyerId),
			() => Assert.Empty(cart.Items),
			() => Assert.True(DateTimeOffset.UtcNow - cart.PurchasedAt < TimeSpan.FromSeconds(1))
		);
	}
}
