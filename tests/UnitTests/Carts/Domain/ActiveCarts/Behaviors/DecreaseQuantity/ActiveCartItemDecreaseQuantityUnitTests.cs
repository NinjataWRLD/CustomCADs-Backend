using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.DecreaseQuantity;

using static ActiveCartsData;

public class ActiveCartItemDecreaseQuantityUnitTests : ActiveCartItemsBaseUnitTests
{
	[Theory]
	[InlineData(MaxValidQuantity)]
	[InlineData(MinValidQuantity)]
	public void Decrease_ShouldNotThrowException_WhenValid(int amount)
	{
		CreateItemWithDelivery()
			.IncreaseQuantity(amount)
			.DecreaseQuantity(amount);
	}

	[Theory]
	[InlineData(MaxValidQuantity)]
	[InlineData(MinValidQuantity)]
	public void Decrease_ShouldThrowException_WhenInvalidAmount(int amount)
	{
		Assert.Throws<CustomValidationException<ActiveCartItem>>(
			() => CreateItemWithDelivery()
				.IncreaseQuantity(amount - 1)
				.DecreaseQuantity(amount)
		);
	}

	[Theory]
	[InlineData(MaxValidQuantity)]
	[InlineData(MinValidQuantity)]
	public void Decrease_ShouldThrowException_WhenNotForDelivery(int amount)
	{
		Assert.Throws<CustomValidationException<ActiveCartItem>>(
			() => CreateItem()
				.IncreaseQuantity(amount)
				.DecreaseQuantity(amount)
		);
	}
}
