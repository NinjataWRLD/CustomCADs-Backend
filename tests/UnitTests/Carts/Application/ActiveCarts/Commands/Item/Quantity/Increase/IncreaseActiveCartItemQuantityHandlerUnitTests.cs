using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Quantity.Increment;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Quantity.Increase.Data;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Quantity.Increase;

using static ActiveCartsData;

public class IncreaseActiveCartItemQuantityHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ProductId productId = ProductId.New(Guid.Empty);
    private readonly ActiveCartItem item;
    private readonly ActiveCart cart;

    public IncreaseActiveCartItemQuantityHandlerUnitTests()
    {
        item = CreateItemWithDelivery(productId: productId);
        cart = CreateCartWithItems(
           buyerId: buyerId,
           items: [item]
       );

        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);
    }

    [Theory]
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldQueryDatabase(int amount)
    {
        // Arrange
        IncreaseActiveCartItemQuantityCommand command = new(buyerId, productId, amount);
        IncreaseActiveCartItemQuantityHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldPersistToDatabase(int amount)
    {
        // Arrange
        IncreaseActiveCartItemQuantityCommand command = new(buyerId, productId, amount);
        IncreaseActiveCartItemQuantityHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldThrowException_WhenCartNotFound(int amount)
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(null as ActiveCart);

        IncreaseActiveCartItemQuantityCommand command = new(buyerId, productId, amount);
        IncreaseActiveCartItemQuantityHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCart>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Theory]
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldThrowException_WhenItemNotFound(int amount)
    {
        // Arrange
        IncreaseActiveCartItemQuantityCommand command = new(
            BuyerId: buyerId,
            ProductId: CartItemsData.ValidProductId1,
            Amount: amount
        );
        IncreaseActiveCartItemQuantityHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
