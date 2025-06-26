namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Carts.Create.WithId;

using static PurchasedCartsData;

public class PurchasedCartCreateWithIdUnitTests : PurchasedCartsBaseUnitTests
{
	[Fact]
	public void CreateWithId_ShouldNotThrowException()
	{
		CreateCartWithId(ValidId, ValidBuyerId);
	}

	[Fact]
	public void CreateWithId_ShouldPopulateProperties()
	{
		var cart = CreateCartWithId(ValidId, ValidBuyerId);

		Assert.Multiple(
			() => Assert.Equal(ValidId, cart.Id),
			() => Assert.Equal(ValidBuyerId, cart.BuyerId),
			() => Assert.Empty(cart.Items),
			() => Assert.True(DateTimeOffset.UtcNow - cart.PurchasedAt < TimeSpan.FromSeconds(1))
		);
	}
}
