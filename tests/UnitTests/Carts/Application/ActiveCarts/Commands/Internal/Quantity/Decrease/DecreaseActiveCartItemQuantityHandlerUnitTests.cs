using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrease;

using Data;
using static ActiveCartItemConstants;
using static ActiveCartsData;

public class DecreaseActiveCartItemQuantityHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly DecreaseActiveCartItemQuantityHandler handler;
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();

    public DecreaseActiveCartItemQuantityHandlerUnitTests()
    {
        handler = new(reads.Object, uow.Object);

        var item = CreateItemWithDelivery(productId: ValidProductId).IncreaseQuantity(QuantityMax - 1);

        reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
            .ReturnsAsync(item);
    }

    [Theory]
    [ClassData(typeof(DecreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldQueryDatabase(int amount)
    {
        // Arrange
        DecreaseActiveCartItemQuantityCommand command = new(
            BuyerId: ValidBuyerId,
            ProductId: ValidProductId,
            Amount: amount
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(DecreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldPersistToDatabase(int amount)
    {
        // Arrange
        DecreaseActiveCartItemQuantityCommand command = new(
            BuyerId: ValidBuyerId,
            ProductId: ValidProductId,
            Amount: amount
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(DecreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldThrowException_WhenCartNotFound(int amount)
    {
        // Arrange
        reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
            .ReturnsAsync(null as ActiveCartItem);

        DecreaseActiveCartItemQuantityCommand command = new(
            BuyerId: ValidBuyerId,
            ProductId: ValidProductId,
            Amount: amount
        );

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Theory]
    [ClassData(typeof(DecreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldThrowException_WhenItemNotFound(int amount)
    {
        // Arrange
        DecreaseActiveCartItemQuantityCommand command = new(
            BuyerId: ValidBuyerId,
            ProductId: ProductId.New(),
            Amount: amount
        );

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
