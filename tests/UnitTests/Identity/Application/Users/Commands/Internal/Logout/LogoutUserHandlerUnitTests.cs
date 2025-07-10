using CustomCADs.Identity.Application.Users.Commands.Internal.Logout;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Logout;

using static UsersData;

public class LogoutUserHandlerUnitTests : UsersBaseUnitTests
{
	private readonly LogoutUserHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IUserWrites> writes = new();

	private static readonly RefreshToken token = RefreshToken.Create("refresh-token", ValidId, longerSession: false);
	private readonly User user = CreateUser(username: MaxValidUsername);

	public LogoutUserHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object);

		reads.Setup(x => x.GetByRefreshTokenAsync(token.Value)).ReturnsAsync((user, token));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		LogoutUserCommand command = new(
			RefreshToken: token.Value
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByRefreshTokenAsync(token.Value), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		LogoutUserCommand command = new(
			RefreshToken: token.Value
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.UpdateRefreshTokensAsync(
			user.Id,
			user.RefreshTokens.ToArray()
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenMissingToken()
	{
		// Arrange
		LogoutUserCommand command = new(RefreshToken: null);

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
		reads.Setup(x => x.GetByRefreshTokenAsync(token.Value)).ReturnsAsync((null, null));
		LogoutUserCommand command = new(token.Value);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
