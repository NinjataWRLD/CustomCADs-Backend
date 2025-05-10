using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increment;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increase;

using Data;
using static ActiveCartsData;

public class IncreaseActiveCartItemQuantityHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly IncreaseActiveCartItemQuantityHandler handler;
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();

    public IncreaseActiveCartItemQuantityHandlerUnitTests()
    {
        handler = new(reads.Object, uow.Object);

        reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
            .ReturnsAsync(CreateItemWithDelivery(productId: ValidProductId));
    }

    [Theory]
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldQueryDatabase(int amount)
    {
        // Arrange
        IncreaseActiveCartItemQuantityCommand command = new(
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
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldPersistToDatabase(int amount)
    {
        // Arrange
        IncreaseActiveCartItemQuantityCommand command = new(
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
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldThrowException_WhenCartNotFound(int amount)
    {
        // Arrange
        reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
            .ReturnsAsync(null as ActiveCartItem);

        IncreaseActiveCartItemQuantityCommand command = new(
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
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldThrowException_WhenItemNotFound(int amount)
    {
        // Arrange
        IncreaseActiveCartItemQuantityCommand command = new(
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
