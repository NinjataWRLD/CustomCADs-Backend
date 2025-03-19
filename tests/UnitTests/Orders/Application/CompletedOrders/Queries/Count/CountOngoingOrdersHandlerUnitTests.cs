using CustomCADs.Orders.Application.CompletedOrders.Queries.Count;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Queries.Count;

using static CompletedOrdersData;

public class CountCompletedOrdersHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly Mock<ICompletedOrderReads> reads = new();

    private const int Count = 5;
    private static readonly AccountId buyerId = ValidBuyerId1;

    public CountCompletedOrdersHandlerUnitTests()
    {
        reads.Setup(x => x.CountByBuyerIdAsync(buyerId, ct))
            .ReturnsAsync(Count);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CountCompletedOrdersQuery query = new(buyerId);
        CountCompletedOrdersHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.CountByBuyerIdAsync(buyerId, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        CountCompletedOrdersQuery query = new(buyerId);
        CountCompletedOrdersHandler handler = new(reads.Object);

        // Act
        int count = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(Count, count);
    }
}
