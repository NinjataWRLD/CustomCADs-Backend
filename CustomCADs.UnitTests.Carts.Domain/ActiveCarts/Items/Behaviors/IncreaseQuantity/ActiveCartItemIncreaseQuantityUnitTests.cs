using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.CartItems;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.IncreaseQuantity.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.IncreaseQuantity;

public class ActiveCartItemIncreaseQuantityUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemIncreaseQuantityValidData))]
    public void Increase_ShouldNotThrowException_WhenValid(int amount)
    {
        CreateItem(forDelivery: true).IncreaseQuantity(amount);
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemIncreaseQuantityInvalidData))]
    public void Increase_ShouldThrowException_WhenInvalidAmount(int amount)
    {
        Assert.Throws<ActiveCartItemValidationException>(() =>
        {
            CreateItem(forDelivery: true).IncreaseQuantity(amount);
        });
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemIncreaseQuantityValidData))]
    public void Increase_ShouldThrowException_WhenNotForDelivery(int amount)
    {
        Assert.Throws<ActiveCartItemValidationException>(() =>
        {
            CreateItem(forDelivery: false).IncreaseQuantity(amount);
        });
    }
}
