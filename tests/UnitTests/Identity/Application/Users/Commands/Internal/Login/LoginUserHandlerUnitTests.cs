using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.Login;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Login;

using static UsersData;

public class LoginUserHandlerUnitTests : UsersBaseUnitTests
{
	private readonly LoginUserHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokens = new();

	private readonly User user = CreateUser(username: MaxValidUsername);

	public LoginUserHandlerUnitTests()
	{
		handler = new(service.Object, tokens.Object);

		const string Token = "refresh-token";
		tokens.Setup(x => x.GenerateRefreshToken()).Returns(Token);
		service.Setup(x => x.AddRefreshTokenAsync(user, Token, false))
			.ReturnsAsync(RefreshToken.Create(Token, user.Id, false));

		service.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
		service.Setup(x => x.CheckPasswordAsync(user.Username, MinValidPassword)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
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
		service.Verify(x => x.GetByUsernameAsync(MaxValidUsername), Times.Once());
		service.Verify(x => x.GetIsLockedOutAsync(MaxValidUsername), Times.Once());
		service.Verify(x => x.CheckPasswordAsync(user.Username, MinValidPassword), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenPasswordIncorrect()
	{
		// Arrange
		service.Setup(x => x.CheckPasswordAsync(user.Username, MinValidPassword)).ReturnsAsync(false);

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
		service.Setup(x => x.GetIsLockedOutAsync(user.Username)).ReturnsAsync(DateTimeOffset.UtcNow);

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
		service.Setup(x => x.GetByUsernameAsync(unverifiedUser.Username)).ReturnsAsync(unverifiedUser);

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
}
