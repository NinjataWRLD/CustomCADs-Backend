using CustomCADs.Orders.Application.OngoingOrders.Commands.Delete;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using System.Xml.Linq;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Delete;

using static OngoingOrdersData;

public class DeleteOngoingOrderHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IWrites<OngoingOrder>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private static readonly OngoingOrderId id = OngoingOrderId.New();
    private static readonly AccountId buyerId = AccountId.New();
    private readonly OngoingOrder order = CreateOrderWithId(id, buyerId: buyerId);

    public DeleteOngoingOrderHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(order);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        DeleteOngoingOrderCommand command = new(
            Id: id,
            BuyerId: buyerId
        );
        DeleteOngoingOrderHandler handler = new(reads.Object, writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        DeleteOngoingOrderCommand command = new(
            Id: id,
            BuyerId: buyerId
        );
        DeleteOngoingOrderHandler handler = new(reads.Object, writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.Remove(
            It.Is<OngoingOrder>(x => x.Id == id)
        ), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
}
