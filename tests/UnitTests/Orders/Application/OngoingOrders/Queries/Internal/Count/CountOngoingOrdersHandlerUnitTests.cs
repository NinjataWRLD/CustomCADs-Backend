using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.Count;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.Internal.Count;

using static OngoingOrdersData;

public class CountOngoingOrdersHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();

    private const int PendingCount = 6;
    private const int AcceptedCount = 5;
    private const int BegunCount = 4;
    private const int FinishedCount = 3;
    private const int ReportedCount = 2;
    private const int RemovedCount = 1;
    private static readonly OngoingOrderId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;

    public CountOngoingOrdersHandlerUnitTests()
    {
        reads.Setup(x => x.CountByStatusAsync(buyerId, ct))
            .ReturnsAsync(new Dictionary<OngoingOrderStatus, int>()
            {
                [OngoingOrderStatus.Pending] = PendingCount,
                [OngoingOrderStatus.Accepted] = AcceptedCount,
                [OngoingOrderStatus.Begun] = BegunCount,
                [OngoingOrderStatus.Finished] = FinishedCount,
                [OngoingOrderStatus.Reported] = ReportedCount,
                [OngoingOrderStatus.Removed] = RemovedCount,
            });
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CountOngoingOrdersQuery query = new(buyerId);
        CountOngoingOrdersHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.CountByStatusAsync(buyerId, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        CountOngoingOrdersQuery query = new(buyerId);
        CountOngoingOrdersHandler handler = new(reads.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(PendingCount, result.Pending),
            () => Assert.Equal(AcceptedCount, result.Accepted),
            () => Assert.Equal(BegunCount, result.Begun),
            () => Assert.Equal(FinishedCount, result.Finished),
            () => Assert.Equal(ReportedCount, result.Reported),
            () => Assert.Equal(RemovedCount, result.Removed)
        );
    }
}
