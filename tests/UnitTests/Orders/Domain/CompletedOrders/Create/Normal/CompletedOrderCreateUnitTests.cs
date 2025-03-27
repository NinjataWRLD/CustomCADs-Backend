using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal.Data;

namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal;

public class CompletedOrderCreateUnitTests : CompletedOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CompletedOrderCreateValidData))]
    public void Create_ShouldNotThrowException_WhenOrderIsValid(string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId)
    {
        CreateOrder(name, description, price, delivery, orderedAt, buyerId);
    }

    [Theory]
    [ClassData(typeof(CompletedOrderCreateValidData))]
    public void Create_ShouldPopulateProperties(string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId)
    {
        var order = CreateOrder(name, description, price, delivery, orderedAt, buyerId);

        Assert.Multiple(
            () => Assert.Equal(name, order.Name),
            () => Assert.Equal(description, order.Description),
            () => Assert.Equal(price, order.Price),
            () => Assert.Equal(delivery, order.Delivery),
            () => Assert.Equal(buyerId, order.BuyerId),
            () => Assert.True(DateTimeOffset.UtcNow.AddSeconds(-1) < order.PurchasedAt)
        );
    }

    [Theory]
    [ClassData(typeof(CompletedOrderCreateInvalidNameData))]
    [ClassData(typeof(CompletedOrderCreateInvalidDescriptionData))]
    [ClassData(typeof(CompletedOrderCreateInvalidPriceData))]
    [ClassData(typeof(CompletedOrderCreateInvalidOrderedAtData))]
    public void Create_ShouldThrowException_WhenOrderIsInvalid(string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId)
    {
        Assert.Throws<CustomValidationException<CompletedOrder>>(() =>
        {
            CreateOrder(name, description, price, delivery, orderedAt, buyerId);
        });
    }
}
