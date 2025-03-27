using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId.Data;

namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId;

public class CompletedOrderCreateWithIdUnitTests : CompletedOrdersBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CompletedOrderCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenOrderIsValid(CompletedOrderId id, string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId)
    {
        CreateOrderWithId(id, name, description, price, delivery, orderedAt, buyerId);
    }

    [Theory]
    [ClassData(typeof(CompletedOrderCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulateProperties(CompletedOrderId id, string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId)
    {
        var order = CreateOrderWithId(id, name, description, price, delivery, orderedAt, buyerId);

        Assert.Multiple(
            () => Assert.Equal(id, order.Id),
            () => Assert.Equal(name, order.Name),
            () => Assert.Equal(description, order.Description),
            () => Assert.Equal(delivery, order.Delivery),
            () => Assert.Equal(buyerId, order.BuyerId),
            () => Assert.True(DateTimeOffset.UtcNow.AddSeconds(-1) < order.PurchasedAt)
        );
    }

    [Theory]
    [ClassData(typeof(CompletedOrderCreateWithIdInvalidNameData))]
    [ClassData(typeof(CompletedOrderCreateWithIdInvalidDescriptionData))]
    [ClassData(typeof(CompletedOrderCreateWithIdInvalidPriceData))]
    [ClassData(typeof(CompletedOrderCreateWithIdInvalidOrderedAtData))]
    public void CreateWithId_ShouldThrowException_WhenOrderIsInvalid(CompletedOrderId id, string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId)
    {
        Assert.Throws<CustomValidationException<CompletedOrder>>(() =>
        {
            CreateOrderWithId(id, name, description, price, delivery, orderedAt, buyerId);
        });
    }
}
