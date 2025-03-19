using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetItem;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.GetItem;

using static PurchasedCartsData;

public class GetPurchasedCartItemByIdUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly Mock<IPurchasedCartReads> reads = new();
    private static readonly PurchasedCart cart = CreateCartWithItems(
        id: id,
        items: [
            CreateItem(productId: CartItemsData.ValidProductId1),
            CreateItem(productId: CartItemsData.ValidProductId2),
        ]
    );
    private static readonly PurchasedCartId id = ValidId1;
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
        GetPurchasedCartItemByIdQuery query = new(id, CartItemsData.ValidProductId1, buyerId);
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
        GetPurchasedCartItemByIdQuery query = new(id, CartItemsData.ValidProductId1, buyerId);
        GetPurchasedCartItemByIdHandler handler = new(reads.Object);

        // Act
        var item = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(CartItemsData.ValidProductId1, item.ProductId);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as PurchasedCart);

        GetPurchasedCartItemByIdQuery query = new(id, CartItemsData.ValidProductId1, buyerId);
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
            ProductId: ProductId.New(),
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
