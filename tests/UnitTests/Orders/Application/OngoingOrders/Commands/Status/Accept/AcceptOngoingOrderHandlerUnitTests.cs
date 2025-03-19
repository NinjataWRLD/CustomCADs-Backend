using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Accept;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Status.Accept;

using static OngoingOrdersData;

public class AcceptOngoingOrderHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();

    private static readonly OngoingOrderId id = ValidId1;
    private static readonly AccountId designerId = ValidDesignerId1;
    private readonly OngoingOrder order = CreateOrder();

    public AcceptOngoingOrderHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
            .ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        AcceptOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        AcceptOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        AcceptOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        AcceptOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        AcceptOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        AcceptOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(It.Is<GetAccountExistsByIdQuery>(x => x.Id == designerId), ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        AcceptOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        AcceptOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(designerId, order.DesignerId),
            () => Assert.Equal(OngoingOrderStatus.Accepted, order.OrderStatus)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDesignerNotFound()
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(It.Is<GetAccountExistsByIdQuery>(x => x.Id == designerId), ct))
            .ReturnsAsync(false);

        AcceptOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        AcceptOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderNotFoundException>(async () =>
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

        AcceptOngoingOrderCommand command = new(
            Id: id,
            DesignerId: designerId
        );
        AcceptOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
