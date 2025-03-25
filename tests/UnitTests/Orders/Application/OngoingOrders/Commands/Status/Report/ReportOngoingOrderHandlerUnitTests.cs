using CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Report;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Status.Report;

using static OngoingOrdersData;

public class ReportOngoingOrderHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private static readonly OngoingOrderId id = ValidId1;
    private static readonly AccountId designerId = ValidDesignerId1;
    private static readonly AccountId wrongDesignerId = ValidDesignerId2;
    private readonly OngoingOrder order = CreateOrder()
        .SetAcceptedStatus().SetDesignerId(designerId);
    private readonly OngoingOrder orderWithPendingStauts = CreateOrder();

    public ReportOngoingOrderHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(order);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ReportOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        ReportOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        ReportOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        ReportOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        ReportOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        ReportOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(designerId, order.DesignerId),
            () => Assert.Equal(OngoingOrderStatus.Reported, order.OrderStatus)
        );
    }

    [Fact]
    public async Task Handle_ShouldNotThrowException_WhenOrderIsPending()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(orderWithPendingStauts);

        ReportOngoingOrderCommand command = new(
            Id: id,
            DesignerId: wrongDesignerId
        );
        ReportOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Act
        // Assert
        await handler.Handle(command, ct);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        ReportOngoingOrderCommand command = new(
            Id: id,
            DesignerId: wrongDesignerId
        );
        ReportOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<CustomAuthorizationException<OngoingOrder>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOrderNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(null as OngoingOrder);

        ReportOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        ReportOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<OngoingOrder>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
