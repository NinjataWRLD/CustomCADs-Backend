using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;

using static ActiveCartsData;

public class GetActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private static readonly AccountId buyerId = ValidBuyerId1;

    public GetActiveCartItemHandlerUnitTests()
    {
        reads.Setup(x => x.SingleAsync(buyerId, ValidProductId1, false, ct))
            .ReturnsAsync(CreateItemWithDelivery(ValidBuyerId1, ValidProductId1));

        reads.Setup(x => x.SingleAsync(buyerId, ValidProductId2, false, ct))
            .ReturnsAsync(CreateItem(ValidBuyerId2, ValidProductId2));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetActiveCartItemQuery query = new(buyerId, ValidProductId1);
        GetActiveCartItemHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleAsync(buyerId, ValidProductId1, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartItemNotFound()
    {
        // Arrange
        GetActiveCartItemQuery query = new(buyerId, ProductId.New());
        GetActiveCartItemHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        GetActiveCartItemQuery query = new(buyerId, ProductId.New());
        GetActiveCartItemHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
