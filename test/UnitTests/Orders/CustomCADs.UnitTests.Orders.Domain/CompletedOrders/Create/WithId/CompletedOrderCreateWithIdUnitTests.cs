using CustomCADs.Orders.Domain.Common.Exceptions.CompletedOrder;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId;

public class CompletedOrderCreateWithIdUnitTests : CompletedOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CompletedOrderCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenOrderIsValid(CompletedOrderId id, string name, string description, decimal price, bool delivery, DateTime orderDate, AccountId buyerId)
    {
        CreateOrderWithId(id, name, description, price, delivery, orderDate, buyerId);
    }

    [Theory]
    [ClassData(typeof(CompletedOrderCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulateProperties(CompletedOrderId id, string name, string description, decimal price, bool delivery, DateTime orderDate, AccountId buyerId)
    {
        var order = CreateOrderWithId(id, name, description, price, delivery, orderDate, buyerId);

        Assert.Multiple(
            () => Assert.Equal(id, order.Id),
            () => Assert.Equal(name, order.Name),
            () => Assert.Equal(description, order.Description),
            () => Assert.Equal(delivery, order.Delivery),
            () => Assert.Equal(buyerId, order.BuyerId),
            () => Assert.True(DateTime.UtcNow.AddSeconds(-1) < order.PurchaseDate)
        );
    }

    [Theory]
    [ClassData(typeof(CompletedOrderCreateWithIdInvalidNameData))]
    [ClassData(typeof(CompletedOrderCreateWithIdInvalidDescriptionData))]
    [ClassData(typeof(CompletedOrderCreateWithIdInvalidPriceData))]
    [ClassData(typeof(CompletedOrderCreateWithIdInvalidOrderDateData))]
    public void CreateWithId_ShouldThrowException_WhenOrderIsInvalid(CompletedOrderId id, string name, string description, decimal price, bool delivery, DateTime orderDate, AccountId buyerId)
    {
        Assert.Throws<CompletedOrderValidationException>(() =>
        {
            CreateOrderWithId(id, name, description, price, delivery, orderDate, buyerId);
        });
    }
}
