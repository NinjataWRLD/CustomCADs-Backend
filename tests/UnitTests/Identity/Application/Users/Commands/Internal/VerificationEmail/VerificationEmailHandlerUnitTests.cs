using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;
using CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;
using CustomCADs.Shared.Abstractions.Events;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.VerificationEmail;

public class VerificationEmailHandlerUnitTests : UsersBaseUnitTests
{
	private readonly VerificationEmailHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const string Token = "email-token";
	private static readonly Func<string, string> getUri = (ect) => $"api.com/{ect}";
	private readonly User user = CreateUser();

	public VerificationEmailHandlerUnitTests()
	{
		handler = new(service.Object, raiser.Object);

		service.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
		service.Setup(x => x.GenerateEmailConfirmationTokenAsync(user.Username)).ReturnsAsync(Token);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		VerificationEmailCommand command = new(user.Username, getUri);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetByUsernameAsync(user.Username), Times.Once());
		service.Verify(x => x.GenerateEmailConfirmationTokenAsync(user.Username), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		VerificationEmailCommand command = new(user.Username, getUri);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<EmailVerificationRequestedApplicationEvent>(x =>
				x.Email == user.Email.Value
			)
		), Times.Once());
	}
}
