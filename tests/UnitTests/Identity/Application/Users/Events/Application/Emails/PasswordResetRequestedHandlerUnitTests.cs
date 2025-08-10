using CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;
using CustomCADs.Shared.Application.Abstractions.Email;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Emails;

public class PasswordResetRequestedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly PasswordResetRequestedEventHandler handler;
	private readonly Mock<IEmailService> email = new();

	private const string Email = "recipient@gmail.com";
	private const string Endpoint = "www.site.com";

	public PasswordResetRequestedHandlerUnitTests()
	{
		handler = new(email.Object);
	}

	[Fact]
	public async Task Handle_ShouldSendEmails()
	{
		// Arrange
		PasswordResetRequestedApplicationEvent ae = new(Email, Endpoint);

		// Act
		await handler.Handle(ae);

		// Assert
		email.Verify(x => x.SendForgotPasswordEmailAsync(Email, Endpoint, ct), Times.Once());
	}
}
