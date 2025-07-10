using CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;
using CustomCADs.Shared.Abstractions.Email;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Emails;

public class EmailVerificationRequestedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly EmailVerificationRequestedEventHandler handler;
	private readonly Mock<IEmailService> email = new();

	private const string Email = "recipient@gmail.com";
	private const string Endpoint = "www.site.com";

	public EmailVerificationRequestedHandlerUnitTests()
	{
		handler = new(email.Object);
	}

	[Fact]
	public async Task Handle_ShouldSendEmails()
	{
		// Arrange
		EmailVerificationRequestedApplicationEvent ae = new(Email, Endpoint);

		// Act
		await handler.Handle(ae);

		// Assert
		email.Verify(x => x.SendVerificationEmailAsync(Email, Endpoint, ct), Times.Once());
	}
}
