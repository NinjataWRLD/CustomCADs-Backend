using CustomCADs.Accounts.Application.Accounts.Events.Application;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application;

using static AccountsData;

public class UserEditedHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly UserEditedHandler handler;
	private readonly Mock<IAccountReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public UserEditedHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateAccount());
	}

	[Fact]
	public async Task Handle_ShoulQueryDatabase()
	{
		// Arrange
		UserEditedApplicationEvent ae = new(
			Id: ValidId,
			Username: null,
			TrackViewedProducts: null
		);

		// Act
		await handler.Handle(ae);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShoulPersistToDatabase()
	{
		// Arrange
		UserEditedApplicationEvent ae = new(
			Id: ValidId,
			Username: null,
			TrackViewedProducts: null
		);

		// Act
		await handler.Handle(ae);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShoulThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Account);
		UserEditedApplicationEvent ae = new(
			Id: ValidId,
			Username: null,
			TrackViewedProducts: null
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(ae)
		);
	}
}
