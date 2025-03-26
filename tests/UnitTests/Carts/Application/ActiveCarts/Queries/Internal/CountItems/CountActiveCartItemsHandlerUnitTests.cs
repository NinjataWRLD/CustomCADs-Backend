using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CountItems;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CountItems;

using static ActiveCartsData;

public class CountActiveCartItemsHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private const int Count = 5;
    private readonly Mock<IActiveCartReads> reads = new();
    private static readonly AccountId buyerId = ValidBuyerId1;

    public CountActiveCartItemsHandlerUnitTests()
    {
        reads.Setup(x => x.CountItemsByBuyerIdAsync(buyerId, ct))
            .ReturnsAsync(Count);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CountActiveCartItemsQuery query = new(buyerId);
        CountActiveCartItemsHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.CountItemsByBuyerIdAsync(buyerId, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        CountActiveCartItemsQuery query = new(buyerId);
        CountActiveCartItemsHandler handler = new(reads.Object);

        // Act
        int count = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(Count, count);
    }
}
