using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increment;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increase.Data;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increase;

using static ActiveCartsData;

public class IncreaseActiveCartItemQuantityHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly IncreaseActiveCartItemQuantityHandler handler;
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ProductId productId = ProductId.New(Guid.Empty);

    public IncreaseActiveCartItemQuantityHandlerUnitTests()
    {
        handler = new(reads.Object, uow.Object);

        reads.Setup(x => x.SingleAsync(buyerId, productId, true, ct))
            .ReturnsAsync(CreateItemWithDelivery(productId: productId));
    }

    [Theory]
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldQueryDatabase(int amount)
    {
        // Arrange
        IncreaseActiveCartItemQuantityCommand command = new(
            BuyerId: buyerId,
            ProductId: productId,
            Amount: amount
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleAsync(buyerId, productId, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(IncreaseActiveCartItemQuantityValidData))]
    public async Task Handle_ShouldPersistToDatabase(int amount)
    {
        // Arrange
        IncreaseActiveCartItemQuantityCommand command = new(
            BuyerId: buyerId,
            ProductId: productId,
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
        reads.Setup(x => x.SingleAsync(buyerId, productId, true, ct))
            .ReturnsAsync(null as ActiveCartItem);

        IncreaseActiveCartItemQuantityCommand command = new(
            BuyerId: buyerId,
            ProductId: productId,
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
            BuyerId: buyerId,
            ProductId: ValidProductId1,
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
