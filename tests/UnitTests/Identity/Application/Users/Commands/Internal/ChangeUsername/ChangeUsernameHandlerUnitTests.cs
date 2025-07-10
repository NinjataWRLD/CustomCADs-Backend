using CustomCADs.Identity.Application.Users.Commands.Internal.ChangeUsername;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ChangeUsername;

using static UsersData;

public class ChangeUsernameHandlerUnitTests : UsersBaseUnitTests
{
	private readonly ChangeUsernameHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IUserWrites> writes = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly User user = CreateUser(username: MaxValidUsername);

	public ChangeUsernameHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, raiser.Object);

		reads.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ChangeUsernameCommand command = new(
			Username: user.Username,
			NewUsername: MinValidUsername
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByUsernameAsync(MaxValidUsername), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ChangeUsernameCommand command = new(
			Username: MaxValidUsername,
			NewUsername: MinValidUsername
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.UpdateUsernameAsync(user.Id, MinValidUsername), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		ChangeUsernameCommand command = new(
			Username: MaxValidUsername,
			NewUsername: MinValidUsername
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserEditedApplicationEvent>(x =>
				x.Username == user.Username
				&& x.Id == user.AccountId
			)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotFound()
	{
		// Arrange
		reads.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(null as User);
		ChangeUsernameCommand command = new(
			Username: MaxValidUsername,
			NewUsername: MinValidUsername
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
