using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.VerifyEmail;

using static UsersData;

public class VerifyUserEmailHandlerUnitTests : UsersBaseUnitTests
{
	private readonly VerifyUserEmailHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IUserWrites> writes = new();
	private readonly Mock<ITokenService> tokens = new();

	private const string Token = "email-token";
	private readonly User user = CreateUser(email: new(ValidEmail, IsVerified: false));

	public VerifyUserEmailHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, tokens.Object);

		reads.Setup(x => x.GetByUsernameAsync(user.Username))
			.ReturnsAsync(user);
		writes.Setup(x => x.ConfirmEmailAsync(user.Username, Token)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		VerifyUserEmailCommand command = new(user.Username, Token);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByUsernameAsync(user.Username), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		VerifyUserEmailCommand command = new(user.Username, Token);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.ConfirmEmailAsync(user.Username, Token), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenConfirmationUnsuccessful()
	{
		// Arrange
		writes.Setup(x => x.ConfirmEmailAsync(user.Username, Token)).ReturnsAsync(false);

		VerifyUserEmailCommand command = new(user.Username, Token);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenEmailVerified()
	{
		// Arrange
		User verifiedUser = CreateUser(email: new(ValidEmail, IsVerified: true));
		reads.Setup(x => x.GetByUsernameAsync(verifiedUser.Username)).ReturnsAsync(verifiedUser);

		VerifyUserEmailCommand command = new(verifiedUser.Username, Token);

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
		VerifyUserEmailCommand command = new(user.Username, Token);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
