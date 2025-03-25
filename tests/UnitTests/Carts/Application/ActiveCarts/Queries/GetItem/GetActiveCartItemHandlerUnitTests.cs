using CustomCADs.Carts.Application.ActiveCarts.Queries.GetItem;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.GetItem;

using static ActiveCartsData;

public class GetActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ActiveCart cart = CreateCartWithItems(
        buyerId: buyerId,
        items: [
            CreateItemWithDelivery(ValidId1, CartItemsData.ValidProductId1),
            CreateItem(ValidId2, CartItemsData.ValidProductId2),
        ]
    );

    public GetActiveCartItemHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, false, ct))
            .ReturnsAsync(cart);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetActiveCartItemByIdQuery query = new(buyerId, CartItemsData.ValidProductId1);
        GetActiveCartItemByIdHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetActiveCartItemByIdQuery query = new(buyerId, CartItemsData.ValidProductId1);
        GetActiveCartItemByIdHandler handler = new(reads.Object);

        // Act
        var actual = await handler.Handle(query, ct);

        // Assert
        var expected = cart.Items.First(x => x.ProductId == CartItemsData.ValidProductId1);
        Assert.Multiple(
            () => Assert.Equal(expected.CartId, actual.CartId),
            () => Assert.Equal(expected.CustomizationId, actual.CustomizationId),
            () => Assert.Equal(expected.Quantity, actual.Quantity),
            () => Assert.Equal(expected.ForDelivery, actual.ForDelivery),
            () => Assert.Equal(expected.ProductId, actual.ProductId)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartItemNotFound()
    {
        // Arrange
        GetActiveCartItemByIdQuery query = new(buyerId, ProductId.New());
        GetActiveCartItemByIdHandler handler = new(reads.Object);

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
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, false, ct))
            .ReturnsAsync(null as ActiveCart);

        GetActiveCartItemByIdQuery query = new(buyerId, ProductId.New());
        GetActiveCartItemByIdHandler handler = new(reads.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCart>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
