using CustomCADs.Carts.Application.ActiveCarts.Commands.Create;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Create;

using static ActiveCartsData;

public class CreateActiveCartHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IWrites<ActiveCart>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly AccountId buyerId = ValidBuyerId1;

    public CreateActiveCartHandlerUnitTests()
    {
        reads.Setup(x => x.ExistsByBuyerIdAsync(buyerId, ct))
            .ReturnsAsync(false);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CreateActiveCartCommand command = new(buyerId);
        CreateActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.ExistsByBuyerIdAsync(buyerId, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase_WhenDoesNotExistYet()
    {
        // Arrange
        CreateActiveCartCommand command = new(buyerId);
        CreateActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<ActiveCart>(x =>
                x.BuyerId == buyerId
            ),
        ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAlreadyExists()
    {
        // Arrange
        reads.Setup(x => x.ExistsByBuyerIdAsync(buyerId, ct))
            .ReturnsAsync(true);

        CreateActiveCartCommand command = new(buyerId);
        CreateActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartAlreadyExistsException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
