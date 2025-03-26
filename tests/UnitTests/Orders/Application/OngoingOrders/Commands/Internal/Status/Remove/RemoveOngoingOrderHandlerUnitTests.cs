using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Remove;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Status.Remove;

using static OngoingOrdersData;

public class RemoveOngoingOrderHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private static readonly OngoingOrderId id = ValidId1;
    private readonly OngoingOrder order = CreateOrder().SetReportedStatus();

    public RemoveOngoingOrderHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(order);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        RemoveOngoingOrderCommand command = new(id);
        RemoveOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        RemoveOngoingOrderCommand command = new(id);
        RemoveOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        RemoveOngoingOrderCommand command = new(id);
        RemoveOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Equal(OngoingOrderStatus.Removed, order.OrderStatus);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOrderNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(null as OngoingOrder);

        RemoveOngoingOrderCommand command = new(id);
        RemoveOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<OngoingOrder>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
