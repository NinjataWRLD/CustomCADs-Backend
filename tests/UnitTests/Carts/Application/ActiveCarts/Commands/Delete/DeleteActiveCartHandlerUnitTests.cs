using CustomCADs.Carts.Application.ActiveCarts.Commands.Delete;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Delete;

using static ActiveCartsData;

public class DeleteActiveCartHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IWrites<ActiveCart>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly ActiveCart cart = CreateCart();
    private readonly AccountId buyerId = ValidBuyerId1;

    public DeleteActiveCartHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        DeleteActiveCartCommand command = new(buyerId);
        DeleteActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase_WhenDoesNotExistYet()
    {
        // Arrange
        DeleteActiveCartCommand command = new(buyerId);
        DeleteActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.Remove(cart), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(null as ActiveCart);

        DeleteActiveCartCommand command = new(buyerId);
        DeleteActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
