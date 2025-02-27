using CustomCADs.Orders.Application.Common.Exceptions.Completed;
using CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;
using CustomCADs.Orders.Domain.CompletedOrders.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Queries.ClientGetById;

using static CompletedOrdersData;

public class ClientGetCompletedOrderByIdHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly Mock<ICompletedOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string TimeZone = "Europe/Sofia";
    private const string Designer = "Oracle3000";
    private static readonly CompletedOrderId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly AccountId wrongBuyerId = ValidBuyerId2;
    private readonly CompletedOrder order = CreateOrderWithId(id);

    public ClientGetCompletedOrderByIdHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZoneByIdQuery>(), ct))
            .ReturnsAsync(TimeZone);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>(), ct))
            .ReturnsAsync(Designer);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ClientGetCompletedOrderByIdQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        ClientGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        ClientGetCompletedOrderByIdQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        ClientGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetTimeZoneByIdQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        ClientGetCompletedOrderByIdQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        ClientGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(order.Id, result.Id),
            () => Assert.Equal(order.Name, result.Name),
            () => Assert.Equal(order.Description, result.Description),
            () => Assert.Equal(order.Delivery, result.Delivery),
            () => Assert.Equal(Designer, result.DesignerName)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenBuyerNotAssociated()
    {
        // Arrange
        ClientGetCompletedOrderByIdQuery query = new(
            Id: id,
            BuyerId: wrongBuyerId
        );
        ClientGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

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

        ClientGetCompletedOrderByIdQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        ClientGetCompletedOrderByIdHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CompletedOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
