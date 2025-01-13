using CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.WithId.Data;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders.Create.WithId;

public class OngoingOrderCreateWithIdUnitTests : OngoingOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(OngoingOrderCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenOrderIsValid(OngoingOrderId id, string name, string description, bool delivery, AccountId buyerId)
    {
        CreateOrderWithId(id, name, description, delivery, buyerId);
    }

    [Theory]
    [ClassData(typeof(OngoingOrderCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulateProperties(OngoingOrderId id, string name, string description, bool delivery, AccountId buyerId)
    {
        var order = CreateOrderWithId(id, name, description, delivery, buyerId);

        Assert.Multiple(
            () => Assert.Equal(name, order.Name),
            () => Assert.Equal(description, order.Description),
            () => Assert.Equal(delivery, order.Delivery),
            () => Assert.Equal(buyerId, order.BuyerId)
        );
    }

    [Theory]
    [ClassData(typeof(OngoingOrderCreateWithIdInvalidNameData))]
    [ClassData(typeof(OngoingOrderCreateWithIdInvalidDescriptionData))]
    public void CreateWithId_ShouldThrowException_WhenOrderIsInvalid(OngoingOrderId id, string name, string description, bool delivery, AccountId buyerId)
    {
        Assert.Throws<OngoingOrderValidationException>(() =>
        {
            CreateOrderWithId(id, name, description, delivery, buyerId);
        });
    }
}
