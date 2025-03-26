using CustomCADs.Accounts.Application.Accounts.Commands.Internal.Delete;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Delete.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Delete;

public class DeleteAccountHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IWrites<Account>> writes = new();
    private readonly Mock<IAccountReads> reads = new();

    [Theory]
    [ClassData(typeof(DeleteAccountValidData))]
    public async Task Handle_ShouldQueryDatabase(string username)
    {
        // Arrange
        Account account = CreateAccount(username: username);
        reads.Setup(x => x.SingleByUsernameAsync(username, true, ct)).ReturnsAsync(account);

        DeleteAccountCommand command = new(username);
        DeleteAccountHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByUsernameAsync(username, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(DeleteAccountValidData))]
    public async Task Handle_ShouldPersistToDatabase_WhenAccountFound(string username)
    {
        // Arrange
        Account account = CreateAccount(username: username);
        reads.Setup(x => x.SingleByUsernameAsync(username, true, ct)).ReturnsAsync(account);

        DeleteAccountCommand command = new(username);
        DeleteAccountHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.Remove(account), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(DeleteAccountValidData))]
    public async Task Handle_ShouldRaiseEvents(string username)
    {
        // Arrange
        Account account = CreateAccount(username: username);
        reads.Setup(x => x.SingleByUsernameAsync(username, true, ct)).ReturnsAsync(account);

        DeleteAccountCommand command = new(username);
        DeleteAccountHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(x => x.RaiseApplicationEventAsync(
            It.Is<AccountDeletedApplicationEvent>(x => x.Username == username)
        ), Times.Once);
    }

    [Theory]
    [ClassData(typeof(DeleteAccountValidData))]
    public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists(string username)
    {
        // Arrange
        reads.Setup(x => x.SingleByUsernameAsync(username, true, ct)).ReturnsAsync(null as Account);

        DeleteAccountCommand command = new(username);
        DeleteAccountHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Account>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
