using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetPrice.Data;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetPrice;

public class OngoingOrderSetPriceUnitTests : OngoingOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(OngoingOrderSetPriceValidData))]
    public void SetPrice_ShouldNotThrowException_WhenOrderValid(decimal price)
    {
        CreateOrder().SetPrice(price);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderSetPriceValidData))]
    public void SetPrice_ShouldPopulateProperly(decimal price)
    {
        var order = CreateOrder();
        order.SetPrice(price);
        Assert.Equal(price, order.Price);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderSetPriceInvalidData))]
    public void SetPrice_ShouldThrowException_WhenPriceInvalid(decimal price)
    {
        Assert.Throws<CustomValidationException<OngoingOrder>>(() =>
        {
            CreateOrder().SetPrice(price);
        });
    }
}
