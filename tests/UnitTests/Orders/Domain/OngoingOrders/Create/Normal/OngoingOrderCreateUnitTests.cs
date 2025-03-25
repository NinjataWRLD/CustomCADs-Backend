using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.Normal.Data;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.Normal;

public class OngoingOrderCreateUnitTests : OngoingOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(OngoingOrderCreateValidData))]
    public void Create_ShouldNotThrowException_WhenOrderIsValid(string name, string description, bool delivery, AccountId buyerId)
    {
        CreateOrder(name, description, delivery, buyerId);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderCreateValidData))]
    public void Create_ShouldPopulateProperties(string name, string description, bool delivery, AccountId buyerId)
    {
        var order = CreateOrder(name, description, delivery, buyerId);

        Assert.Multiple(
            () => Assert.Equal(name, order.Name),
            () => Assert.Equal(description, order.Description),
            () => Assert.Equal(delivery, order.Delivery),
            () => Assert.Equal(buyerId, order.BuyerId)
        );
    }

    [Theory]
    [ClassData(typeof(OngoingOrderCreateInvalidNameData))]
    [ClassData(typeof(OngoingOrderCreateInvalidDescriptionData))]
    public void Create_ShouldThrowException_WhenOrderIsInvalid(string name, string description, bool delivery, AccountId buyerId)
    {
        Assert.Throws<CustomValidationException<OngoingOrder>>(() =>
        {
            CreateOrder(name, description, delivery, buyerId);
        });
    }
}
