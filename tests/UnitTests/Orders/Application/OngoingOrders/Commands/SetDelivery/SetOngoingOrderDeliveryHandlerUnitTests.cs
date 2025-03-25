using CustomCADs.Orders.Application.OngoingOrders.Commands.SetDelivery;
using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.SetDelivery;

using static OngoingOrdersData;

public class SetOngoingOrderDeliveryHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private static readonly OngoingOrderId id = OngoingOrderId.New();
    private static readonly bool value = true;
    private static readonly AccountId buyerId = AccountId.New();
    private readonly OngoingOrder order = CreateOrder(delivery: !value);

    public SetOngoingOrderDeliveryHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(order);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        SetOngoingOrderDeliveryCommand command = new(
            Id: id,
            Value: value,
            BuyerId: buyerId
        );
        SetOngoingOrderDeliveryHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToyDatabase()
    {
        // Arrange
        SetOngoingOrderDeliveryCommand command = new(
            Id: id,
            Value: value,
            BuyerId: buyerId
        );
        SetOngoingOrderDeliveryHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        SetOngoingOrderDeliveryCommand command = new(
            Id: id,
            Value: value,
            BuyerId: buyerId
        );
        SetOngoingOrderDeliveryHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Equal(value, order.Delivery);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOrderNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(null as OngoingOrder);

        SetOngoingOrderDeliveryCommand command = new(
            Id: id,
            Value: value,
            BuyerId: buyerId
        );
        SetOngoingOrderDeliveryHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<OngoingOrder>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
