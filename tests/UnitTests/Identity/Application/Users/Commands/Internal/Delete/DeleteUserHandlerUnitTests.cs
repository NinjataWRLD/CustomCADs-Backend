using CustomCADs.Identity.Application.Users.Commands.Internal.Delete;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Delete;

using static UsersData;

public class DeleteUserHandlerUnitTests : UsersBaseUnitTests
{
	private readonly DeleteUserHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IUserWrites> writes = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly User user = CreateUser(username: MaxValidUsername);

	public DeleteUserHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, raiser.Object);

		reads.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DeleteUserCommand command = new(user.Username);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByUsernameAsync(MaxValidUsername), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteUserCommand command = new(user.Username);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.DeleteAsync(user.Username), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		DeleteUserCommand command = new(user.Username);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserDeletedApplicationEvent>(x => x.Id == user.AccountId)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotFound()
	{
		// Arrange
		reads.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(null as User);
		DeleteUserCommand command = new(user.Username);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
