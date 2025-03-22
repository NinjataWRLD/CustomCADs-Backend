using CustomCADs.Orders.Domain.CompletedOrders.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal.Data;

namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal;

public class CompletedOrderCreateUnitTests : CompletedOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CompletedOrderCreateValidData))]
    public void Create_ShouldNotThrowException_WhenOrderIsValid(string name, string description, decimal price, bool delivery, DateTime orderDate, AccountId buyerId)
    {
        CreateOrder(name, description, price, delivery, orderDate, buyerId);
    }

    [Theory]
    [ClassData(typeof(CompletedOrderCreateValidData))]
    public void Create_ShouldPopulateProperties(string name, string description, decimal price, bool delivery, DateTime orderDate, AccountId buyerId)
    {
        var order = CreateOrder(name, description, price, delivery, orderDate, buyerId);

        Assert.Multiple(
            () => Assert.Equal(name, order.Name),
            () => Assert.Equal(description, order.Description),
            () => Assert.Equal(price, order.Price),
            () => Assert.Equal(delivery, order.Delivery),
            () => Assert.Equal(buyerId, order.BuyerId),
            () => Assert.True(DateTime.UtcNow.AddSeconds(-1) < order.PurchaseDate)
        );
    }

    [Theory]
    [ClassData(typeof(CompletedOrderCreateInvalidNameData))]
    [ClassData(typeof(CompletedOrderCreateInvalidDescriptionData))]
    [ClassData(typeof(CompletedOrderCreateInvalidPriceData))]
    [ClassData(typeof(CompletedOrderCreateInvalidOrderDateData))]
    public void Create_ShouldThrowException_WhenOrderIsInvalid(string name, string description, decimal price, bool delivery, DateTime orderDate, AccountId buyerId)
    {
        Assert.Throws<CompletedOrderValidationException>(() =>
        {
            CreateOrder(name, description, price, delivery, orderDate, buyerId);
        });
    }
}
