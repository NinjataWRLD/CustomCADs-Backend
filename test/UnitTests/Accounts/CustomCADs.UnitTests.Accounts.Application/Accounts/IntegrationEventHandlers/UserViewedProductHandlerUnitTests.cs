using CustomCADs.Accounts.Application.Accounts.IntegrationEventHandlers;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.IntegrationEvents.Catalog;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.IntegrationEventHandlers;

public class UserViewedProductHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IAccountReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private static readonly AccountId id = AccountId.New();
    private static readonly ProductId productId = ProductId.New();
    private readonly Account account = CreateAccount();

    public UserViewedProductHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(account);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        UserViewedProductIntegrationEvent ie = new(id, productId);
        UserViewedProductHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        UserViewedProductIntegrationEvent ie = new(id, productId);
        UserViewedProductHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        UserViewedProductIntegrationEvent ie = new(id, productId);
        UserViewedProductHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        Assert.Contains(account.ViewedProductIds, x => x == productId);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAccountNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(null as Account);

        UserViewedProductIntegrationEvent ie = new(id, productId);
        UserViewedProductHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(ie);
        });
    }
}
