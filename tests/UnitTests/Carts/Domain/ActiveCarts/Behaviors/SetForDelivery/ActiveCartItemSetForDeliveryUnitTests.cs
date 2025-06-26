namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.SetForDelivery;

using static ActiveCartsData;

public class ActiveCartItemSetForDeliveryUnitTests : ActiveCartItemsBaseUnitTests
{
	[Fact]
	public void SetForDelivery_ShouldNotThrowException()
	{
		CreateItem().SetForDelivery(ValidCustomizationId);
	}

	[Fact]
	public void SetForDelivery_ShouldPopulateProperties()
	{
		var item = CreateItem();
		item.SetForDelivery(ValidCustomizationId);
		Assert.Multiple(
			() => Assert.True(item.ForDelivery),
			() => Assert.Equal(ValidCustomizationId, item.CustomizationId)
		);
	}
}
