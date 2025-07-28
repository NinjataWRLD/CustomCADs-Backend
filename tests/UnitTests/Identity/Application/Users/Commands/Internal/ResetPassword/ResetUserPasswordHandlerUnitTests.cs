using CustomCADs.Identity.Application.Users.Commands.Internal.ResetPassword;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ResetPassword;

using static UsersData;

public class ResetUserPasswordHandlerUnitTests : UsersBaseUnitTests
{
	private readonly ResetUserPasswordHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IUserWrites> writes = new();

	private const string Token = "email-token";
	private readonly User user = CreateUser();

	public ResetUserPasswordHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object);

		reads.Setup(x => x.GetByEmailAsync(user.Email.Value))
			.ReturnsAsync(user);
		writes.Setup(x => x.ResetPasswordAsync(user.Username, Token, MinValidPassword)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ResetUserPasswordCommand command = new(
			Email: user.Email.Value,
			Token: Token,
			NewPassword: MinValidPassword
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByEmailAsync(user.Email.Value), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ResetUserPasswordCommand command = new(
			Email: user.Email.Value,
			Token: Token,
			NewPassword: MinValidPassword
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.ResetPasswordAsync(user.Username, Token, MinValidPassword), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenResetUnsuccessful()
	{
		// Arrange
		writes.Setup(x => x.ResetPasswordAsync(user.Username, Token, MinValidPassword)).ReturnsAsync(false);

		ResetUserPasswordCommand command = new(
			Email: user.Email.Value,
			Token: Token,
			NewPassword: MinValidPassword
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
		reads.Setup(x => x.GetByEmailAsync(user.Email.Value)).ReturnsAsync(null as User);
		ResetUserPasswordCommand command = new(
			Email: user.Email.Value,
			Token: Token,
			NewPassword: MinValidPassword
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
