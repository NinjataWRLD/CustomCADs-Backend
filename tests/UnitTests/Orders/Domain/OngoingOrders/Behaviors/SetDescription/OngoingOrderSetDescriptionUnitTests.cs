using CustomCADs.Orders.Domain.OngoingOrders.Exceptions;
using CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetDescription.Data;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetDescription;

public class OngoingOrderSetDescriptionUnitTests : OngoingOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(OngoingOrderSetDescriptionValidData))]
    public void SetDescription_ShouldNotThrowException_WhenOrderValid(string description)
    {
        CreateOrder().SetDescription(description);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderSetDescriptionValidData))]
    public void SetDescription_ShouldPopulateProperly(string description)
    {
        var order = CreateOrder();
        order.SetDescription(description);
        Assert.Equal(description, order.Description);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderSetDescriptionInvalidData))]
    public void SetDescription_ShouldThrowException_WhenDescriptionInvalid(string description)
    {
        Assert.Throws<OngoingOrderValidationException>(() =>
        {
            CreateOrder().SetDescription(description);
        });
    }
}
