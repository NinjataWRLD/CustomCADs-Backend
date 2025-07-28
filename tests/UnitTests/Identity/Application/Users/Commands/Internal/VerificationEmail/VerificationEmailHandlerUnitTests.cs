using CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;
using CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.VerificationEmail;

public class VerificationEmailHandlerUnitTests : UsersBaseUnitTests
{
	private readonly VerificationEmailHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IUserWrites> writes = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const string Token = "email-token";
	private static readonly Func<string, string> getUri = (ect) => $"api.com/{ect}";
	private readonly User user = CreateUser();

	public VerificationEmailHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, raiser.Object);

		reads.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(user);
		writes.Setup(x => x.GenerateEmailConfirmationTokenAsync(user.Username)).ReturnsAsync(Token);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		VerificationEmailCommand command = new(user.Username, getUri);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByUsernameAsync(user.Username), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		VerificationEmailCommand command = new(user.Username, getUri);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.GenerateEmailConfirmationTokenAsync(user.Username), Times.Once());
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

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotFound()
	{
		// Arrange
		reads.Setup(x => x.GetByUsernameAsync(user.Username)).ReturnsAsync(null as User);
		VerificationEmailCommand command = new(user.Username, getUri);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
