using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.ToggleForDelivery;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.ToggleForDelivery;

using static ActiveCartsData;

public class ToggleActiveCartItemForDeliveryHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ActiveCartItemId itemId = ActiveCartItemId.New(Guid.Empty);
    private readonly ActiveCartItem item;
    private readonly ActiveCart cart;

    public ToggleActiveCartItemForDeliveryHandlerUnitTests()
    {
        item = CreateItemWithId(itemId);
        cart = CreateCartWithItems(
           buyerId: buyerId,
           items: [item]
       );

        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ToggleActiveCartItemForDeliveryCommand command = new(buyerId, itemId);
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
        ToggleActiveCartItemForDeliveryCommand command = new(buyerId, itemId);
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

        ToggleActiveCartItemForDeliveryCommand command = new(buyerId, itemId);
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
            ItemId: CartItemsData.ValidId1
        );
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartItemNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
