using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.VerifyEmail;

using static UsersData;

public class VerifyUserEmailHandlerUnitTests : UsersBaseUnitTests
{
	private readonly VerifyUserEmailHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokens = new();

	private const string Token = "email-token";
	private readonly User user = CreateUser(email: new(ValidEmail, IsVerified: false));

	public VerifyUserEmailHandlerUnitTests()
	{
		handler = new(service.Object, tokens.Object);

		tokens.Setup(x => x.GenerateRefreshToken()).Returns(Token);
		service.Setup(x => x.AddRefreshTokenAsync(user, Token, false))
			.ReturnsAsync(RefreshToken.Create(Token, user.Id, false));

		service.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		VerifyUserEmailCommand command = new(user.Username, Token);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetByUsernameAsync(user.Username), Times.Once());
		service.Verify(x => x.ConfirmEmailAsync(user.Username, Token), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenEmailVerified()
	{
		// Arrange
		User verifiedUser = CreateUser(email: new(ValidEmail, IsVerified: true));
		service.Setup(x => x.GetByUsernameAsync(verifiedUser.Username)).ReturnsAsync(verifiedUser);

		VerifyUserEmailCommand command = new(verifiedUser.Username, Token);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
