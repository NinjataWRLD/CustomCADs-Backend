using CustomCADs.Carts.Domain.ActiveCarts.Exceptions.CartItems;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.DecreaseQuantity.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.DecreaseQuantity;

public class ActiveCartItemDecreaseQuantityUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemDecreaseQuantityValidData))]
    public void Decrease_ShouldNotThrowException_WhenValid(int amount)
    {
        CreateItemWithDelivery()
            .IncreaseQuantity(amount)
            .DecreaseQuantity(amount);
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemDecreaseQuantityValidData))]
    public void Decrease_ShouldThrowException_WhenInvalidAmount(int amount)
    {
        Assert.Throws<ActiveCartItemValidationException>(() =>
        {
            CreateItemWithDelivery()
                .IncreaseQuantity(amount - 1)
                .DecreaseQuantity(amount);
        });
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemDecreaseQuantityValidData))]
    public void Decrease_ShouldThrowException_WhenNotForDelivery(int amount)
    {
        Assert.Throws<ActiveCartItemValidationException>(() =>
        {
            CreateItem()
                .IncreaseQuantity(amount)
                .DecreaseQuantity(amount);
        });
    }
}
