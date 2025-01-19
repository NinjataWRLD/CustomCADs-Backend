using CustomCADs.Orders.Application.Common.Exceptions.Completed;
using CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;
using CustomCADs.Orders.Domain.CompletedOrders.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Queries.DesignerGetById;

using static CompletedOrdersData;

public class DesignerGetCompletedOrderByIdHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly Mock<ICompletedOrderReads> reads = new();

    private static readonly CompletedOrderId id = ValidId1;
    private static readonly AccountId designerId = ValidDesignerId1;
    private static readonly AccountId wrongDesignerId = ValidDesignerId2;
    private readonly CompletedOrder order = CreateOrderWithId(id, designerId: designerId);

    public DesignerGetCompletedOrderByIdHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(order);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        DesignerGetCompletedOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetCompletedOrderByIdHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        DesignerGetCompletedOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetCompletedOrderByIdHandler handler = new(reads.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(result.Id, order.Id),
            () => Assert.Equal(result.Name, order.Name),
            () => Assert.Equal(result.Description, order.Description),
            () => Assert.Equal(result.Delivery, order.Delivery),
            () => Assert.Equal(result.BuyerId, order.BuyerId)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDesignerNotAssociated()
    {
        // Arrange
        DesignerGetCompletedOrderByIdQuery query = new(
            Id: id,
            DesignerId: wrongDesignerId
        );
        DesignerGetCompletedOrderByIdHandler handler = new(reads.Object);

        // Assert
        await Assert.ThrowsAsync<CompletedOrderAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOrderNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as CompletedOrder);

        DesignerGetCompletedOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetCompletedOrderByIdHandler handler = new(reads.Object);

        // Assert
        await Assert.ThrowsAsync<CompletedOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
