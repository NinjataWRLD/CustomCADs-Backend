using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetName.Data;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Behaviors.SetName;

public class OngoingOrderSetNameUnitTests : OngoingOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(OngoingOrderSetNameValidData))]
    public void SetName_ShouldNotThrowException_WhenOrderValid(string name)
    {
        CreateOrder().SetName(name);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderSetNameValidData))]
    public void SetName_ShouldPopulateProperly(string name)
    {
        var order = CreateOrder();
        order.SetName(name);
        Assert.Equal(name, order.Name);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderSetNameInvalidData))]
    public void SetName_ShouldThrowException_WhenNameInvalid(string name)
    {
        Assert.Throws<CustomValidationException<OngoingOrder>>(() =>
        {
            CreateOrder().SetName(name);
        });
    }
}
