using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.ToggleForDelivery;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.ToggleForDelivery;

using static ActiveCartsData;

public class ToggleActiveCartItemForDeliveryHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ProductId productId1 = ProductId.New();
    private static readonly ProductId productId2 = ProductId.New();
    private readonly ActiveCart cart;

    public ToggleActiveCartItemForDeliveryHandlerUnitTests()
    {
        cart = CreateCartWithItems(
           buyerId: buyerId,
           items: [
               CreateItemWithDelivery(productId: productId1),
               CreateItem(productId: productId2),
           ]
       );

        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ToggleActiveCartItemForDeliveryCommand command = new(buyerId, productId1, null);
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        ToggleActiveCartItemForDeliveryCommand command = new(buyerId, productId1, null);
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(null as ActiveCart);

        ToggleActiveCartItemForDeliveryCommand command = new(buyerId, productId1, null);
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenItemNotFound()
    {
        // Arrange
        ToggleActiveCartItemForDeliveryCommand command = new(
            BuyerId: buyerId,
            ProductId: CartItemsData.ValidProductId1,
            CustomizationId: null
        );
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartItemNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDeliveryMismatch()
    {
        // Arrange
        ToggleActiveCartItemForDeliveryCommand command = new(
            BuyerId: buyerId,
            ProductId: productId2,
            CustomizationId: null
        );
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartItemDeliveryException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
