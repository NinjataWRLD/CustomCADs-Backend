using CustomCADs.Orders.Application.Common.Exceptions.Completed;
using CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.GetCadUrlGet;
using CustomCADs.Orders.Domain.CompletedOrders.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Queries.GetCadUrlGet;

using static CompletedOrdersData;

public class GetCompletedOrderCadPresignedUrlGetHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly Mock<ICompletedOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string Url = "https://presigned.url";
    private static readonly CompletedOrderId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly AccountId wrongBuyerId = ValidBuyerId2;
    private readonly CompletedOrder order = CreateOrderWithId(id);

    public GetCompletedOrderCadPresignedUrlGetHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadPresignedUrlGetByIdQuery>(), ct))
            .ReturnsAsync(Url);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetCompletedOrderCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        GetCompletedOrderCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetCompletedOrderCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        GetCompletedOrderCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCadPresignedUrlGetByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetCompletedOrderCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        GetCompletedOrderCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(Url, result.PresignedUrl);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenBuyerNotAssociated()
    {
        // Arrange
        GetCompletedOrderCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: wrongBuyerId
        );
        GetCompletedOrderCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

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

        GetCompletedOrderCadPresignedUrlGetQuery query = new(
            Id: id,
            BuyerId: buyerId
        );
        GetCompletedOrderCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CompletedOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
