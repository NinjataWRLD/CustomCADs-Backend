using CustomCADs.Carts.Domain.ActiveCarts.Exceptions.CartItems;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.IncreaseQuantity.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.IncreaseQuantity;

public class ActiveCartItemIncreaseQuantityUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemIncreaseQuantityValidData))]
    public void Increase_ShouldNotThrowException_WhenValid(int amount)
    {
        CreateItemWithDelivery().IncreaseQuantity(amount);
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemIncreaseQuantityInvalidData))]
    public void Increase_ShouldThrowException_WhenInvalidAmount(int amount)
    {
        Assert.Throws<ActiveCartItemValidationException>(() =>
        {
            CreateItemWithDelivery().IncreaseQuantity(amount);
        });
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemIncreaseQuantityValidData))]
    public void Increase_ShouldThrowException_WhenNotForDelivery(int amount)
    {
        Assert.Throws<ActiveCartItemValidationException>(() =>
        {
            CreateItem().IncreaseQuantity(amount);
        });
    }
}
