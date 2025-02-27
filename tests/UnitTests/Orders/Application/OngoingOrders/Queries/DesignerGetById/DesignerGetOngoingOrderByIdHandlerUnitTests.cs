using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Application.OngoingOrders.Queries.DesignerGetById;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.DesignerGetById;

using static OngoingOrdersData;

public class DesignerGetOngoingOrderByIdHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string Buyer = "ivanzlatinov1";
    private static readonly OngoingOrderId id = ValidId1;
    private static readonly AccountId designerId = ValidDesignerId1;
    private static readonly AccountId wrongDesignerId = ValidDesignerId2;
    private readonly OngoingOrder order =
        CreateOrderWithId(id)
        .SetAcceptedStatus()
        .SetDesignerId(designerId);

    public DesignerGetOngoingOrderByIdHandlerUnitTests()
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
        DesignerGetOngoingOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        DesignerGetOngoingOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        DesignerGetOngoingOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(order.Id, result.Id),
            () => Assert.Equal(order.Name, result.Name),
            () => Assert.Equal(order.Description, result.Description),
            () => Assert.Equal(order.Delivery, result.Delivery),
            () => Assert.Equal(order.OrderStatus, result.OrderStatus),
            () => Assert.Equal(Buyer, result.BuyerName)
        );
    }

    [Fact]
    public async Task Handle_ShouldNotThrowException_WhenStatusIsPending()
    {
        // Arrange
        order.SetPendingStatus();

        DesignerGetOngoingOrderByIdQuery query = new(
            Id: id,
            DesignerId: wrongDesignerId
        );
        DesignerGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        // Assert
        await handler.Handle(query, ct);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDesignerNotAssociated()
    {
        // Arrange
        DesignerGetOngoingOrderByIdQuery query = new(
            Id: id,
            DesignerId: wrongDesignerId
        );
        DesignerGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderAuthorizationException>(async () =>
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
            .ReturnsAsync(null as OngoingOrder);

        DesignerGetOngoingOrderByIdQuery query = new(
            Id: id,
            DesignerId: designerId
        );
        DesignerGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
