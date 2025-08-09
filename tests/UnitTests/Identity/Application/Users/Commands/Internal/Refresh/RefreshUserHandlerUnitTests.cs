using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Refresh;

using static UsersData;

public class RefreshUserHandlerUnitTests : UsersBaseUnitTests
{
	private readonly RefreshUserHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IUserWrites> writes = new();
	private readonly Mock<ITokenService> tokens = new();

	private static readonly RefreshToken token = RefreshToken.Create("refresh-token", ValidId, longerSession: false);
	private readonly User user = CreateUser(username: MaxValidUsername);

	public RefreshUserHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, tokens.Object);

		reads.Setup(x => x.GetByRefreshTokenAsync(token.Value)).ReturnsAsync((user, token));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		RefreshUserCommand command = new(token.Value);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByRefreshTokenAsync(token.Value), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenMissingToken()
	{
		// Arrange
		RefreshUserCommand command = new(Token: null);

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
		RefreshUserCommand command = new(token.Value);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenTokenExpired()
	{
		// Arrange
		DateTimeOffset yesterday = DateTimeOffset.UtcNow.AddDays(-1);
		RefreshToken token = RefreshToken.Create(
			id: RefreshTokenId.New(),
			value: "refresh-token",
			userId: ValidId,
			issuedAt: yesterday.AddDays(-Constants.Tokens.RtDurationInDays),
			expiresAt: yesterday
		);
		reads.Setup(x => x.GetByRefreshTokenAsync(token.Value)).ReturnsAsync((user, token));

		RefreshUserCommand command = new(token.Value);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
