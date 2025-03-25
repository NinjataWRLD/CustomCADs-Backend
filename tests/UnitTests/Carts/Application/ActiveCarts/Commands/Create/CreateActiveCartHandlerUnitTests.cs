using CustomCADs.Carts.Application.ActiveCarts.Commands.Create;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Create;

using static ActiveCartsData;

public class CreateActiveCartHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IWrites<ActiveCart>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly AccountId buyerId = ValidBuyerId1;

    public CreateActiveCartHandlerUnitTests()
    {
        reads.Setup(x => x.ExistsByBuyerIdAsync(buyerId, ct))
            .ReturnsAsync(false);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
            .ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CreateActiveCartCommand command = new(buyerId);
        CreateActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.ExistsByBuyerIdAsync(buyerId, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        CreateActiveCartCommand command = new(buyerId);
        CreateActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object, sender.Object);

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
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CreateActiveCartCommand command = new(buyerId);
        CreateActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetAccountExistsByIdQuery>(x => x.Id == buyerId),
        ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenBuyerDoesNotExist()
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(
            It.Is<GetAccountExistsByIdQuery>(x => x.Id == buyerId), ct)
        ).ReturnsAsync(false);

        CreateActiveCartCommand command = new(buyerId);
        CreateActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCart>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAlreadyExists()
    {
        // Arrange
        reads.Setup(x => x.ExistsByBuyerIdAsync(buyerId, ct))
            .ReturnsAsync(true);

        CreateActiveCartCommand command = new(buyerId);
        CreateActiveCartHandler handler = new(reads.Object, writes.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
