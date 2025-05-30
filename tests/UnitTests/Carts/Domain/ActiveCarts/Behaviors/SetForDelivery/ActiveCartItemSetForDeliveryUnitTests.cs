namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.SetForDelivery;

using static ActiveCartsData;

public class ActiveCartItemSetForDeliveryUnitTests : ActiveCartItemsBaseUnitTests
{
	[Fact]
	public void SetForDelivery_ShouldNotThrow()
	{
		CreateItem().SetForDelivery(ValidCustomizationId);
	}

	[Fact]
	public void SetForDelivery_ShouldPopulateProperly()
	{
		var item = CreateItem();
		item.SetForDelivery(ValidCustomizationId);
		Assert.Multiple(
			() => Assert.True(item.ForDelivery),
			() => Assert.Equal(ValidCustomizationId, item.CustomizationId)
		);
	}
}
