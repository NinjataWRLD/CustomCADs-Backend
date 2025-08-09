using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using Microsoft.Extensions.Options;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

public class ResetUserPasswordEmailHandlerUnitTests : UsersBaseUnitTests
{
	private readonly ResetPasswordEmailHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Mock<IOptions<ClientUrlSettings>> settings = new();

	private const string Token = "password-token";
	private const string PreferredUrl = "preferred-url";
	private readonly User user = CreateUser();

	public ResetUserPasswordEmailHandlerUnitTests()
	{
		settings.Setup(x => x.Value).Returns(new ClientUrlSettings(string.Empty, PreferredUrl));
		handler = new(service.Object, raiser.Object, settings.Object);

		service.Setup(x => x.GeneratePasswordResetTokenAsync(user.Email.Value)).ReturnsAsync(Token);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		ResetPasswordEmailCommand command = new(user.Email.Value);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GeneratePasswordResetTokenAsync(user.Email.Value), Times.Once());
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
}
