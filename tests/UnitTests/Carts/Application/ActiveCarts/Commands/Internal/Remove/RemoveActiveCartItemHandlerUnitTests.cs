using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Remove;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Remove;

using static ActiveCartsData;

public class RemoveActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly RemoveActiveCartItemHandler handler;
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IWrites<ActiveCartItem>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly ActiveCartItem item = CreateItem(productId: ValidProductId);

    public RemoveActiveCartItemHandlerUnitTests()
    {
        handler = new(reads.Object, writes.Object, uow.Object);

        reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
            .ReturnsAsync(item);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        RemoveActiveCartItemCommand command = new(
            BuyerId: ValidBuyerId,
            ProductId: ValidProductId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct));
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        RemoveActiveCartItemCommand command = new(
            BuyerId: ValidBuyerId,
            ProductId: ValidProductId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct));
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
            .ReturnsAsync(null as ActiveCartItem);

        RemoveActiveCartItemCommand command = new(
            BuyerId: ValidBuyerId,
            ProductId: ValidProductId
        );

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
            // Act
            async () => await handler.Handle(command, ct)
        );
    }
}
