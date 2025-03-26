using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Item.Remove;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Remove;

using static ActiveCartsData;

public class RemoveActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly ActiveCart cart = CreateCartWithItems(
        buyerId: ValidBuyerId1,
        items: [CreateItem(productId: productId)]
    );
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ProductId productId = ProductId.New(Guid.Empty);

    public RemoveActiveCartItemHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        RemoveActiveCartItemCommand command = new(
            BuyerId: buyerId,
            ProductId: productId
        );
        RemoveActiveCartItemHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, true, ct));
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        RemoveActiveCartItemCommand command = new(
            BuyerId: buyerId,
            ProductId: productId
        );
        RemoveActiveCartItemHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct));
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        RemoveActiveCartItemCommand command = new(
            BuyerId: buyerId,
            ProductId: productId
        );
        RemoveActiveCartItemHandler handler = new(reads.Object, uow.Object);
        int beforeCount = cart.Items.Count;

        // Act
        await handler.Handle(command, ct);
        int afterCount = cart.Items.Count;

        // Assert
        Assert.Equal(afterCount, beforeCount - 1);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(null as ActiveCart);

        RemoveActiveCartItemCommand command = new(
            BuyerId: buyerId,
            ProductId: productId
        );
        RemoveActiveCartItemHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCart>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
