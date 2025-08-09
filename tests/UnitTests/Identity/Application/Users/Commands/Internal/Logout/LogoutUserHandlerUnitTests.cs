using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.Logout;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Logout;

using static UsersData;

public class LogoutUserHandlerUnitTests : UsersBaseUnitTests
{
	private readonly LogoutUserHandler handler;
	private readonly Mock<IUserService> service = new();

	private static readonly RefreshToken token = RefreshToken.Create("refresh-token", ValidId, longerSession: false);
	private readonly User user = CreateUser(username: MaxValidUsername);

	public LogoutUserHandlerUnitTests()
	{
		handler = new(service.Object);

		service.Setup(x => x.GetByRefreshTokenAsync(token.Value)).ReturnsAsync((user, token));
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		LogoutUserCommand command = new(
			RefreshToken: token.Value
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.RevokeRefreshTokenAsync(token.Value), Times.Once());
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
}
