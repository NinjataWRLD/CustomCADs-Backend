using CustomCADs.Identity.Application.Users.Commands.Internal.Login;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Tokens;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Login;

using static UsersData;

public class LoginUserHandlerUnitTests : UsersBaseUnitTests
{
	private readonly LoginUserHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IUserWrites> writes = new();
	private readonly Mock<ITokenService> tokens = new();

	private readonly User user = CreateUser(username: MaxValidUsername);

	public LoginUserHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, tokens.Object);

		reads.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
		writes.Setup(x => x.CheckPasswordAsync(user.Username, MinValidPassword)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		LoginUserCommand command = new(
			Username: user.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByUsernameAsync(MaxValidUsername), Times.Once());
		reads.Verify(x => x.GetIsLockedOutAsync(MaxValidUsername), Times.Once());
		writes.Verify(x => x.CheckPasswordAsync(user.Username, MinValidPassword), Times.Once()); // this is a read operation, but due to potential side-effect is defined a write operation
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenPasswordIncorrect()
	{
		// Arrange
		writes.Setup(x => x.CheckPasswordAsync(user.Username, MinValidPassword)).ReturnsAsync(false);

		LoginUserCommand command = new(
			Username: user.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserLockedOut()
	{
		// Arrange
		reads.Setup(x => x.GetIsLockedOutAsync(user.Username)).ReturnsAsync(DateTimeOffset.UtcNow);

		LoginUserCommand command = new(
			Username: user.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotVerified()
	{
		// Arrange
		User unverifiedUser = CreateUser(email: new(ValidEmail, IsVerified: false));
		reads.Setup(x => x.GetByUsernameAsync(unverifiedUser.Username)).ReturnsAsync(unverifiedUser);

		LoginUserCommand command = new(
			Username: unverifiedUser.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotFound()
	{
		// Arrange
		reads.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(null as User);
		LoginUserCommand command = new(
			Username: user.Username,
			Password: MinValidPassword,
			LongerExpireTime: false
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
