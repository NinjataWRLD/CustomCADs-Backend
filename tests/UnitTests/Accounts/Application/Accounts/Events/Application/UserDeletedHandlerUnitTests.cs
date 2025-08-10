using CustomCADs.Accounts.Application.Accounts.Events.Application;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Events.Identity;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application;

using static AccountsData;

public class UserDeletedHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly UserDeletedHandler handler;
	private readonly Mock<IAccountReads> reads = new();
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Account account = CreateAccount();

	public UserDeletedHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(account);
	}

	[Fact]
	public async Task Handle_ShoulQueryDatabase()
	{
		// Arrange
		UserDeletedApplicationEvent ae = new(ValidId);

		// Act
		await handler.Handle(ae);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShoulPersistToDatabase()
	{
		// Arrange
		UserDeletedApplicationEvent ae = new(ValidId);

		// Act
		await handler.Handle(ae);

		// Assert
		writes.Verify(x => x.Remove(account), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShoulThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Account);
		UserDeletedApplicationEvent ae = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(ae)
		);
	}
}
