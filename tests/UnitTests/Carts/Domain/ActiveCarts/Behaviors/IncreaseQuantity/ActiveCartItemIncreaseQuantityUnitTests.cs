using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.IncreaseQuantity.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Behaviors.IncreaseQuantity;

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
        Assert.Throws<CustomValidationException<ActiveCartItem>>(() =>
        {
            CreateItemWithDelivery().IncreaseQuantity(amount);
        });
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemIncreaseQuantityValidData))]
    public void Increase_ShouldThrowException_WhenNotForDelivery(int amount)
    {
        Assert.Throws<CustomValidationException<ActiveCartItem>>(() =>
        {
            CreateItem().IncreaseQuantity(amount);
        });
    }
}
