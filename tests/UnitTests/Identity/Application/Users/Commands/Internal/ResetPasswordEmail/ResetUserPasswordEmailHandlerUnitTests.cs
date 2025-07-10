using CustomCADs.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using Microsoft.Extensions.Options;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

public class ResetUserPasswordEmailHandlerUnitTests : UsersBaseUnitTests
{
	private readonly ResetPasswordEmailHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IUserWrites> writes = new();
	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Mock<IOptions<ClientUrlSettings>> settings = new();

	private const string Token = "password-token";
	private const string PreferredUrl = "preferred-url";
	private readonly User user = CreateUser();

	public ResetUserPasswordEmailHandlerUnitTests()
	{
		settings.Setup(x => x.Value).Returns(new ClientUrlSettings(string.Empty, PreferredUrl));
		handler = new(reads.Object, writes.Object, raiser.Object, settings.Object);

		reads.Setup(x => x.GetByEmailAsync(user.Email.Value)).ReturnsAsync(user);
		writes.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(Token);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ResetPasswordEmailCommand command = new(user.Email.Value);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByEmailAsync(user.Email.Value), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ResetPasswordEmailCommand command = new(user.Email.Value);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.GeneratePasswordResetTokenAsync(user), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		ResetPasswordEmailCommand command = new(user.Email.Value);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<PasswordResetRequestedApplicationEvent>(x =>
				x.Email == user.Email.Value
				&& x.Endpoint.Contains(user.Email.Value)
				&& x.Endpoint.Contains(Token)
				&& x.Endpoint.Contains(PreferredUrl)
			)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotFound()
	{
		// Arrange
		reads.Setup(x => x.GetByEmailAsync(user.Email.Value)).ReturnsAsync(null as User);
		ResetPasswordEmailCommand command = new(user.Email.Value);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
