using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.IncreaseQuantity;

using static ActiveCartsData;

public class ActiveCartItemIncreaseQuantityUnitTests : ActiveCartItemsBaseUnitTests
{
	[Theory]
	[InlineData(MaxValidQuantity)]
	[InlineData(MinValidQuantity)]
	public void Increase_ShouldNotThrowException_WhenValid(int amount)
	{
		CreateItemWithDelivery().IncreaseQuantity(amount);
	}

	[Theory]
	[InlineData(MaxInvalidQuantity)]
	[InlineData(MinInvalidQuantity)]
	public void Increase_ShouldThrowException_WhenInvalidAmount(int amount)
	{
		Assert.Throws<CustomValidationException<ActiveCartItem>>(
			() => CreateItemWithDelivery().IncreaseQuantity(amount)
		);
	}

	[Theory]
	[InlineData(MaxValidQuantity)]
	[InlineData(MinValidQuantity)]
	public void Increase_ShouldThrowException_WhenNotForDelivery(int amount)
	{
		Assert.Throws<CustomValidationException<ActiveCartItem>>(
			() => CreateItem().IncreaseQuantity(amount)
		);
	}
}
