using CustomCADs.Orders.Application.Common.Exceptions.Completed;
using CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Queries.DesignerGetById;

using static CompletedOrdersData;

public class DesignerGetCompletedOrderByIdHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly Mock<ICompletedOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string Buyer = "For7a7a";
    private static readonly CompletedOrderId id = ValidId1;
    private static readonly AccountId designerId = ValidDesignerId1;
    private static readonly AccountId wrongDesignerId = ValidDesignerId2;
    private readonly CompletedOrder order = CreateOrderWithId(id, designerId: designerId);

    public DesignerGetCompletedOrderByIdHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>(), ct))
            .ReturnsAsync(Buyer);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        DesignerGetCompletedOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        DesignerGetCompletedOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x =>
            x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        DesignerGetCompletedOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(order.Id, result.Id),
            () => Assert.Equal(order.Name, result.Name),
            () => Assert.Equal(order.Description, result.Description),
            () => Assert.Equal(order.Delivery, result.Delivery),
            () => Assert.Equal(Buyer, result.BuyerName)
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
        DesignerGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

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
        DesignerGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CompletedOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
