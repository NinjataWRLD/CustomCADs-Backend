using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetItem;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.GetItem;

using static PurchasedCartsData;

public class GetPurchasedCartItemByIdUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly Mock<IPurchasedCartReads> reads = new();
    private static readonly PurchasedCart cart = CreateCartWithItems(
        id: id,
        items: [CreateItem(), CreateItem(), CreateItem()]
    );
    private static readonly PurchasedCartId id = ValidId1;
    private static readonly PurchasedCartItemId itemId = PurchasedCartItemId.New(Guid.Empty);
    private static readonly AccountId buyerId = ValidBuyerId1;

    public GetPurchasedCartItemByIdUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(cart);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetPurchasedCartItemByIdQuery query = new(id, itemId, buyerId);
        GetPurchasedCartItemByIdHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetPurchasedCartItemByIdQuery query = new(id, itemId, buyerId);
        GetPurchasedCartItemByIdHandler handler = new(reads.Object);

        // Act
        var item = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(itemId, item.Id);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as PurchasedCart);

        GetPurchasedCartItemByIdQuery query = new(id, itemId, buyerId);
        GetPurchasedCartItemByIdHandler handler = new(reads.Object);

        // Assert
        await Assert.ThrowsAsync<PurchasedCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartItemNotFound()
    {
        // Arrange
        GetPurchasedCartItemByIdQuery query = new(
            Id: id,
            ItemId: CartItemsData.ValidId2,
            BuyerId: buyerId
        );
        GetPurchasedCartItemByIdHandler handler = new(reads.Object);

        // Assert
        await Assert.ThrowsAsync<PurchasedCartItemNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
