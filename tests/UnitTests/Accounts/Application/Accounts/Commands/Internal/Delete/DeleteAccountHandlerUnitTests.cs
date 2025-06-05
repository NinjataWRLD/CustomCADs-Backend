using CustomCADs.Accounts.Application.Accounts.Commands.Internal.Delete;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Delete;

using CustomCADs.Accounts.Domain.Repositories.Writes;
using Data;

public class DeleteAccountHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly DeleteAccountHandler handler;
	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IAccountReads> reads = new();

	public DeleteAccountHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);
	}

	[Theory]
	[ClassData(typeof(DeleteAccountValidData))]
	public async Task Handle_ShouldQueryDatabase(string username)
	{
		// Arrange
		Account account = CreateAccount(username: username);
		reads.Setup(x => x.SingleByUsernameAsync(username, true, ct)).ReturnsAsync(account);
		DeleteAccountCommand command = new(username);

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

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
