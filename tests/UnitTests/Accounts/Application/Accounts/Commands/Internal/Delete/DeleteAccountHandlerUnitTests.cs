using CustomCADs.Accounts.Application.Accounts.Commands.Internal.Delete;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Delete;

using CustomCADs.Accounts.Domain.Repositories.Writes;
using static AccountsData;

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

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateAccountWithId());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DeleteAccountCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteAccountCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(It.Is<Account>(x => x.Id == ValidId)), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		DeleteAccountCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<AccountDeletedApplicationEvent>(x => x.Username == ValidUsername)
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountDoesNotExists()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Account);
		DeleteAccountCommand command = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
