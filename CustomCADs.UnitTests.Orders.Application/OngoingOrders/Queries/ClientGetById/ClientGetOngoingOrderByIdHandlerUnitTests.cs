using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Application.OngoingOrders.Queries.ClientGetById;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.ClientGetById;

using static OngoingOrdersData;

public class ClientGetOngoingOrderByIdHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string TimeZone = "Europe/Sofia";
    private static readonly OngoingOrderId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly AccountId wrongBuyerId = ValidBuyerId2;
    private readonly OngoingOrder order = CreateOrderWithId(id);

    public ClientGetOngoingOrderByIdHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZoneByIdQuery>(), ct))
            .ReturnsAsync(TimeZone);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ClientGetOngoingOrderByIdQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        ClientGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        ClientGetOngoingOrderByIdQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        ClientGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetTimeZoneByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        ClientGetOngoingOrderByIdQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        ClientGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(result.Id, order.Id),
            () => Assert.Equal(result.Name, order.Name),
            () => Assert.Equal(result.Description, order.Description),
            () => Assert.Equal(result.Delivery, order.Delivery),
            () => Assert.Equal(result.OrderStatus, order.OrderStatus),
            () => Assert.Equal(result.DesignerId, order.DesignerId)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenBuyerNotAssociated()
    {
        // Arrange
        ClientGetOngoingOrderByIdQuery query = new(
            Id: id,
            BuyerId: wrongBuyerId
        );
        ClientGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

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

        ClientGetOngoingOrderByIdQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        ClientGetOngoingOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
